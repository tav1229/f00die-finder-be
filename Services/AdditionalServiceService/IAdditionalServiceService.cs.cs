using f00die_finder_be.Dtos.Restaurant;

namespace f00die_finder_be.Services.AdditionalServiceService
{
    public interface IAdditionalServiceService
    {
        Task<List<AdditionalServiceDto>> GetAditionalServicesAsync();
        Task<List<AdditionalServiceDto>> GetAditionalServicesByRestaurantAsync(Guid restaurantId);
    }
}
