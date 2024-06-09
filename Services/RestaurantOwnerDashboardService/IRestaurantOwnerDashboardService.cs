using f00die_finder_be.Dtos;

namespace f00die_finder_be.Services.RestaurantOwnerDashboardService
{
    public interface IRestaurantOwnerDashboardService
    {
        Task<CustomResponse<object>> GetTotalReservationsAsync();
        Task<CustomResponse<Dictionary<int, int>>> GetReservationsByMonth();
        Task<CustomResponse<object>> GetDistinctUserReservationCount();

    }
}
