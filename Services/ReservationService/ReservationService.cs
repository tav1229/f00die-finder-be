using f00die_finder_be.Common;
using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Reservation;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.ReservationService
{
    public class ReservationService : BaseService, IReservationService
    {
        public ReservationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<CustomResponse<ReservationDetailDto>> AddAsync(ReservationAddDto reservationAddDto)
        {
            var restaurantQuery = await _unitOfWork.GetQueryableAsync<Restaurant>();
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

            await _cacheService.RemoveAsync($"reservations-restaurant-{reservation.RestaurantId}");
            
            return new CustomResponse<ReservationDetailDto>
            {
                Data = _mapper.Map<ReservationDetailDto>(reservation)
            };
        }

        public async Task<CustomResponse<ReservationDetailDto>> GetReservationByIdAsync(Guid reservationId)
        {
            var data = await _cacheService.GetOrCreateAsync($"reservation-{reservationId}", async () =>
            {
                var reservationQuery = await _unitOfWork.GetQueryableAsync<Reservation>();
                var reservation = await reservationQuery
                    .FirstOrDefaultAsync(r => r.Id == reservationId);

                return _mapper.Map<ReservationDetailDto>(reservation);
            });

            return new CustomResponse<ReservationDetailDto>
            {
                Data = data
            };
        }

        public async Task<CustomResponse<List<ReservationDto>>> GetReservationsOfRestaurantAsync(FilterReservationsOfRestaurantDto filter, int pageSize, int pageNumber)
        {
            var cacheKey = $"reservations-restaurant-{filter.RestaurantId}";
            var restaurantReservations = await _cacheService.GetOrCreateAsync(cacheKey, async () =>
            {
                var reservationQuery = await _unitOfWork.GetQueryableAsync<Reservation>();
                return await reservationQuery
                    .Where(r => r.RestaurantId == filter.RestaurantId)
                    .ToListAsync();
            });
            var restaurantReservationsQuery = restaurantReservations.AsQueryable();
            if (filter.ReservationStatus != null)
            {
                restaurantReservationsQuery = restaurantReservationsQuery.Where(r => r.ReservationStatus == filter.ReservationStatus);
            }
            if (filter.ReservationTime != null)
            {
                restaurantReservationsQuery = restaurantReservationsQuery.Where(r => r.ReservationTime.Date == filter.ReservationTime.Value.Date);
            }

            int totalItems = restaurantReservationsQuery.Count();
            var items = restaurantReservationsQuery
                .OrderByDescending(r => r.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => _mapper.Map<ReservationDto>(r))
                .ToList();

            return new CustomResponse<List<ReservationDto>>
            {
                Data = items,
                Meta = new MetaData
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
                }
            };
        }

        public async Task<CustomResponse<ReservationDetailDto>> UpdateReservationStatusAsync(Guid reservationId, ReservationStatus reservationStatus)
        {
            var reservationQuery = await _unitOfWork.GetQueryableAsync<Reservation>();
            var reservation = await reservationQuery.FirstOrDefaultAsync(r => r.Id == reservationId);
            if (reservation == null)
            {
                throw new NotFoundException();
            }

            reservation.ReservationStatus = reservationStatus;
            await _unitOfWork.UpdateAsync(reservation);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveAsync($"reservations-restaurant-{reservation.RestaurantId}");
            await _cacheService.RemoveAsync($"reservation-{reservationId}");

            return new CustomResponse<ReservationDetailDto>
            {
                Data = _mapper.Map<ReservationDetailDto>(reservation)
            };
        }
    }
}
