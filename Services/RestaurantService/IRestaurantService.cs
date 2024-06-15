using f00die_finder_be.Common;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Restaurant;

namespace f00die_finder_be.Services.RestaurantService
{
    public interface IRestaurantService
    {
        Task<CustomResponse<List<RestaurantDto>>> GetRestaurantsAsync(FilterRestaurantDto? filter, RestaurantSortType? sortType, int pageSize, int pageNumber);
        Task<CustomResponse<RestaurantDetailDto>> GetRestaurantByIdAsync(Guid restaurantId);
        Task<CustomResponse<RestaurantDetailDto>> GetMyRestaurantAsync();
        Task<CustomResponse<RestaurantDetailDto>> AddAsync(RestaurantAddDto restaurant);
        Task<CustomResponse<RestaurantDetailDto>> UpdateAsync(RestaurantUpdateDto restaurant);
        Task<CustomResponse<RestaurantDetailDto>> AddImagesAsync (RestaurantAddImagesDto restaurant);
        Task<CustomResponse<RestaurantDetailDto>> DeleteImagesAsync (List<Guid> imageIds);
        Task<CustomResponse<RestaurantDetailDto>> DeactivateMyRestaurantAsync();
        Task<CustomResponse<List<UserSavedRestaurantDto>>> GetMySavedRestaurantsAsync(int pageSize, int pageNumber);
        Task<CustomResponse<object>> SaveRestaurantAsync(Guid restaurantId);
        Task<CustomResponse<object>> UnsaveRestaurantAsync(Guid restaurantId);
        Task<CustomResponse<object>> ChangeRestaurantStatusAdminAsync(Guid restaurantId, RestaurantStatus status);
        Task<CustomResponse<List<RestaurantAdminDto>>> GetRestaurantsAdminAsync(FilterRestaurantAdminDto? filter, int pageSize, int pageNumber);
    }
}