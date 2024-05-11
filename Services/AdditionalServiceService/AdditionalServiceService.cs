using f00die_finder_be.Dtos.Restaurant;
using f00die_finder_be.Entities;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.AdditionalServiceService
{
    public class AdditionalServiceService : BaseService, IAdditionalServiceService 
    {
        public AdditionalServiceService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<AdditionalServiceDto>> GetAditionalServicesAsync()
        {
            return await _cacheService.GetOrCreateAsync("additionalServices", async () =>
            {
                var additionalServiceQuery = await _unitOfWork.GetAllAsync<AdditionalService>();

                var additionalServices = await additionalServiceQuery.ToListAsync();

                return _mapper.Map<List<AdditionalServiceDto>>(additionalServices);
            });
        }

        public async Task<List<AdditionalServiceDto>> GetAditionalServicesByRestaurantAsync(Guid restaurantId)
        {
            return await _cacheService.GetOrCreateAsync($"additionalServices-restaurant-{restaurantId}", async () =>
            {
                var additionalServiceQuery = await _unitOfWork.GetAllAsync<RestaurantAdditionalService>();

                var additionalServices = await additionalServiceQuery
                    .Where(s => s.RestaurantId == restaurantId)
                    .Select(s => s.AdditionalService).ToListAsync();

                return _mapper.Map<List<AdditionalServiceDto>>(additionalServices);
            });
        }
    }
}
