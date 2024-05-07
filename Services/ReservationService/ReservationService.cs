using AutoMapper;
using f00die_finder_be.Common;
using f00die_finder_be.Common.CurrentUserService;
using f00die_finder_be.Data;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Reservation;
using f00die_finder_be.Models;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ReservationService(DataContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public async Task<Guid> AddAsync(ReservationAddDto reservationAddDto)
        {
            var restaurant = await _context.Restaurants.FindAsync(reservationAddDto.RestaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException();
            }

            var reservation = _mapper.Map<Reservation>(reservationAddDto);
            reservation.RestaurantId = reservationAddDto.RestaurantId;
            reservation.UserId = _currentUserService.UserId;
            reservation.ReservationStatus = ReservationStatus.Pending;

            restaurant.ReservationCount++;

            _context.Reservations.Add(reservation);
            _context.Restaurants.Update(restaurant);

            await _context.SaveChangesAsync();

            return reservation.Id;
        }

        public async Task<ReservationDetailDto> GetReservationByIdAsync(Guid reservationId)
        {
            var reservation =  await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == reservationId);
            if (reservation == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<ReservationDetailDto>(reservation);
        }

        public async Task<PagedResult<ReservationDto>> GetReservationsOfRestaurantAsync(Guid restaurantId, ReservationStatus? reservationStatus, DateTime? time, int pageSize, int pageNumber)
        {
            var reservations = _context.Reservations
                .Where(r => r.RestaurantId == restaurantId);
            if (reservationStatus != null)
            {
                reservations = reservations.Where(r => r.ReservationStatus == reservationStatus);
            }
            if (time != null)
            {
                reservations = reservations.Where(r => r.Time.Date == time.Value.Date);
            }

            return new PagedResult<ReservationDto>
            {
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(await reservations.CountAsync() / (double)pageSize),
                Items = await reservations
                    .OrderByDescending(r => r.CreatedDate)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(r => _mapper.Map<ReservationDto>(r))
                    .ToListAsync(),
            };
        }

        public async Task UpdateReservationStatusAsync(Guid reservationId, ReservationStatus reservationStatus)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null)
            {
                throw new NotFoundException();
            }

            reservation.ReservationStatus = reservationStatus;
            await _context.SaveChangesAsync();  
        }
    }
}
