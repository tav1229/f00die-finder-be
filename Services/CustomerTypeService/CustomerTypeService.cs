using f00die_finder_be.Dtos.Restaurant;
using f00die_finder_be.Entities;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.CustomerTypeService
{
    public class CustomerTypeService : BaseService, ICustomerTypeService
    {
        public CustomerTypeService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<CustomerTypeDto>> GetCustomerTypesAsync()
        {
            return await _cacheService.GetOrCreateAsync("customerTypes", async () =>
            {
                var customerTypeQuery = await _unitOfWork.GetAllAsync<CustomerType>();
                var customerTypes = await customerTypeQuery.ToListAsync();

                return _mapper.Map<List<CustomerTypeDto>>(customerTypes);
            });
        }

        public async Task<List<CustomerTypeDto>> GetCustomerTypesByRestaurantAsync(Guid restaurantId)
        {
            return await _cacheService.GetOrCreateAsync($"customerTypes-restaurant-{restaurantId}", async () =>
            {
                var customerTypeQuery = await _unitOfWork.GetAllAsync<RestaurantCustomerType>();
                var customerTypes = await customerTypeQuery
                    .Where(s => s.RestaurantId == restaurantId)
                    .Select(s => s.CustomerType).ToListAsync();

                return _mapper.Map<List<CustomerTypeDto>>(customerTypes);
            });
        }
    }
}
