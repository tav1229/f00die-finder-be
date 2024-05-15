using f00die_finder_be.Dtos.Restaurant;

namespace f00die_finder_be.Services.CustomerTypeService
{
    public interface ICustomerTypeService
    {
        Task<List<CustomerTypeDto>> GetCustomerTypesAsync();
        Task<List<CustomerTypeDto>> GetCustomerTypesByRestaurantAsync(Guid restaurantId);
    }
}
