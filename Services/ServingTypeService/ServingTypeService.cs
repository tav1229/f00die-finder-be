using f00die_finder_be.Dtos.Restaurant;
using f00die_finder_be.Entities;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.ServingTypeService
{
    public class ServingTypeService : BaseService, IServingTypeService
    {
        public ServingTypeService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<ServingTypeDto>> GetServingTypesAsync()
        {
            return await _cacheService.GetOrCreateAsync("servingTypes", async () =>
            {
                var servingTypeQuery = await _unitOfWork.GetAllAsync<ServingType>();
                var servingTypes = await servingTypeQuery.ToListAsync();

                return _mapper.Map<List<ServingTypeDto>>(servingTypes);
            });
        }

        public async Task<List<ServingTypeDto>> GetServingTypesByRestaurantAsync(Guid restaurantId)
        {
            return await _cacheService.GetOrCreateAsync($"servingTypes-restaurant-{restaurantId}", async () =>
            {
                var servingTypeQuery = await _unitOfWork.GetAllAsync<RestaurantServingType>();
                var servingTypes = await servingTypeQuery
                    .Where(s => s.RestaurantId == restaurantId)
                    .Select(s => s.ServingType).ToListAsync();

                return _mapper.Map<List<ServingTypeDto>>(servingTypes);
            });
        }
    }
}
