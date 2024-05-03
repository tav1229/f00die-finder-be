using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Restaurant;

namespace f00die_finder_be.Services.RestaurantService
{
    public interface IRestaurantService
    {
        Task<PagedResult<RestaurantDto>> GetRestaurantsAsync(FilterRestaurantDto? filterRestaurantDto, string searchValue, int pageSize, int pageNumber);
        Task<RestaurantDetailDto> GetRestaurantByIdAsync(Guid restaurantId);
        Task<RestaurantDetailDto> GetRestaurantByOwnerId(Guid userId);
        Task<Guid> AddAsync(RestaurantAddDto restaurant);
        Task UpdateAsync(RestaurantUpdateDto restaurant);
        Task DeleteAsync(Guid restaurantId);
    }
}