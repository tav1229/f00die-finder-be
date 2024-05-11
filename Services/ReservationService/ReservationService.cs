using f00die_finder_be.Common;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Reservation;
using f00die_finder_be.Entities;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.ReservationService
{
    public class ReservationService : BaseService, IReservationService
    {
        public ReservationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<Guid> AddAsync(ReservationAddDto reservationAddDto)
        {
            var restaurantQuery = await _unitOfWork.GetAllAsync<Restaurant>();
            var restaurant = await restaurantQuery.FirstOrDefaultAsync(r => r.Id == reservationAddDto.RestaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException();
            }

            var reservation = _mapper.Map<Reservation>(reservationAddDto);
            reservation.RestaurantId = reservationAddDto.RestaurantId;
            reservation.UserId = _currentUserService.UserId;
            reservation.ReservationStatus = ReservationStatus.Pending;

            restaurant.ReservationCount++;

            await _unitOfWork.AddAsync(reservation);
            await _unitOfWork.UpdateAsync(restaurant);

            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveByPrefixAsync("reservations");
            return reservation.Id;
        }

        public async Task<ReservationDetailDto> GetReservationByIdAsync(Guid reservationId)
        {
            return await _cacheService.GetOrCreateAsync($"reservation-{reservationId}", async () =>
            {
                var reservationQuery = await _unitOfWork.GetAllAsync<Reservation>();
                var reservation = await reservationQuery
                    .FirstOrDefaultAsync(r => r.Id == reservationId);

                return _mapper.Map<ReservationDetailDto>(reservation);
            });
        }

        public async Task<PagedResult<ReservationDto>> GetReservationsOfRestaurantAsync(Guid restaurantId, ReservationStatus? reservationStatus, DateTime? time, int pageSize, int pageNumber)
        {
            return await _cacheService.GetOrCreateAsync($"reservations-restaurant-{restaurantId}", async () =>
            {
                var reservationQuery = await _unitOfWork.GetAllAsync<Reservation>();
                var reservations = reservationQuery
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
            });
        }

        public async Task UpdateReservationStatusAsync(Guid reservationId, ReservationStatus reservationStatus)
        {
            var reservationQuery = await _unitOfWork.GetAllAsync<Reservation>();
            var reservation = await reservationQuery.FirstOrDefaultAsync(r => r.Id == reservationId);
            if (reservation == null)
            {
                throw new NotFoundException();
            }

            reservation.ReservationStatus = reservationStatus;
            await _unitOfWork.UpdateAsync(reservation);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveByPrefixAsync($"reservation");
        }
    }
}
