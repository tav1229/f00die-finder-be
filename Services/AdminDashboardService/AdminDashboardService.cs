using f00die_finder_be.Common;
using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.AdminDashboardService
{
    public class AdminDashboardService : BaseService, IAdminDashboardService
    {
        public AdminDashboardService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<CustomResponse<Dictionary<Role, int>>> GetNumberOfUsersByRole()
        {
            var usersByRole = await (await _unitOfWork.GetQueryableAsync<User>())
                .GroupBy(x => x.Role)
                .Select(x => new
                {
                    Role = x.Key,
                    Count = x.Count()
                })
                .ToListAsync();

            var completeUsersByRole = usersByRole.ToDictionary(x => x.Role, x => x.Count);

            return new CustomResponse<Dictionary<Role, int>>
            {
                Data = completeUsersByRole
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

        public async Task<CustomResponse<object>> GetTotalRestaurantsAsync()
        {
            var totalRestaurants = await (await _unitOfWork.GetQueryableAsync<Restaurant>()).CountAsync();
            return new CustomResponse<object>
            {
                Data = new
                {
                    TotalRestaurants = totalRestaurants
                }
            };
        }

        public async Task<CustomResponse<object>> GetTotalUsersAsync()
        {
            var totalUsers = await (await _unitOfWork.GetQueryableAsync<User>()).CountAsync();
            return new CustomResponse<object>
            {
                Data = new
                {
                    TotalUsers = totalUsers
                }
            };
        }
    }
}
