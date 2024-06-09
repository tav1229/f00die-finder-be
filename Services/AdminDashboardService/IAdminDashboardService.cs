using f00die_finder_be.Dtos;

namespace f00die_finder_be.Services.AdminDashboardService
{
    public interface IAdminDashboardService
    {
        Task<CustomResponse<object>> GetTotalRestaurantsAsync();
        Task<CustomResponse<object>> GetTotalUsersAsync();
        Task<CustomResponse<object>> GetTotalReservationsAsync();
        Task<CustomResponse<Dictionary<int, int>>> GetReservationsByMonth();
    }
}
