using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.RestaurantOwnerDashboardService
{
    public class RestaurantOwnerDashboardService : BaseService, IRestaurantOwnerDashboardService
    {
        public RestaurantOwnerDashboardService(IServiceProvider provider) : base(provider)
        {
        }

        public async Task<CustomResponse<object>> GetDistinctUserReservationCount()
        {
            var restaurant = await _unitOfWork.GetQueryableAsync<Restaurant>();

            var myRestaurant = restaurant.FirstOrDefault(r => r.OwnerId == _currentUserService.UserId);


            var reservationQuery = await _unitOfWork.GetQueryableAsync<Reservation>();

            var distinctUserCount = reservationQuery
                .Where(x => x.RestaurantId == myRestaurant.Id)
                .Select(x => x.UserId)
                .Distinct()
                .Count();

            return new CustomResponse<object>
            {
                Data = new
                {
                    DistinctUserCount = distinctUserCount
                }
            };

        }

        public async Task<CustomResponse<Dictionary<int, int>>> GetReservationsByMonth()
        {
            var reservationsByMonth = await (await _unitOfWork.GetQueryableAsync<Reservation>())
                .GroupBy(x => x.CreatedDate.Month)
                .Select(x => new
                {
                    Month = x.Key,
                    Count = x.Count()
                })
                .ToListAsync();

            var completeReservationsByMonth = Enumerable.Range(1, 12)
                .ToDictionary(
                    month => month,
                    month => reservationsByMonth.FirstOrDefault(x => x.Month == month)?.Count ?? 0
                );

            return new CustomResponse<Dictionary<int, int>>
            {
                Data = completeReservationsByMonth
            };
        }

        public async Task<CustomResponse<object>> GetTotalReservationsAsync()
        {
            var totalReservations = await (await _unitOfWork.GetQueryableAsync<Reservation>()).CountAsync();
            return new CustomResponse<object>
            {
                Data = new
                {
                    TotalReservations = totalReservations
                }
            };
        }
    }
}
