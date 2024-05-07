using f00die_finder_be.Dtos.Restaurant;

namespace f00die_finder_be.Services.CuisineTypeService
{
    public interface ICuisineTypeService
    {
        Task<List<CuisineTypeDto>> GetCuisineTypesAsync();
        Task<List<CuisineTypeDto>> GetCuisineTypesByRestaurantAsync(Guid restaurantId);
    }
}
