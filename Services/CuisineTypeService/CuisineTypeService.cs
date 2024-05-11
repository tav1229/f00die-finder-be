using f00die_finder_be.Dtos.Restaurant;
using f00die_finder_be.Entities;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.CuisineTypeService
{
    public class CuisineTypeService : BaseService, ICuisineTypeService
    {
        public CuisineTypeService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<CuisineTypeDto>> GetCuisineTypesAsync()
        {
            return await _cacheService.GetOrCreateAsync("cuisineTypes", async () =>
            {
                var cuisineTypeQuery = await _unitOfWork.GetAllAsync<CuisineType>();
                var cuisineTypes = await cuisineTypeQuery.ToListAsync();

                return _mapper.Map<List<CuisineTypeDto>>(cuisineTypes);
            });
        }

        public async Task<List<CuisineTypeDto>> GetCuisineTypesByRestaurantAsync(Guid restaurantId)
        {
            return await _cacheService.GetOrCreateAsync($"cuisineTypes-restaurant-{restaurantId}", async () =>
            {
                var cuisineTypeQuery = await _unitOfWork.GetAllAsync<RestaurantCuisineType>();
                var cuisineTypes = await cuisineTypeQuery
                    .Where(s => s.RestaurantId == restaurantId)
                    .Select(s => s.CuisineType).ToListAsync();

                return _mapper.Map<List<CuisineTypeDto>>(cuisineTypes);
            });
        }
    }
}
