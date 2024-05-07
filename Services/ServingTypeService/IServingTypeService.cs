using f00die_finder_be.Dtos.Restaurant;

namespace f00die_finder_be.Services.ServingTypeService
{
    public interface IServingTypeService
    {
        Task<List<ServingTypeDto>> GetServingTypesAsync();
        Task<List<ServingTypeDto>> GetServingTypesByRestaurantAsync(Guid restaurantId);
    }
}
