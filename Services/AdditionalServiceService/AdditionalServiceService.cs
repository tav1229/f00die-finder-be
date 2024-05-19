using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.AdditionalServiceService
{
    public class AdditionalServiceService : BaseService, IAdditionalServiceService 
    {
        public AdditionalServiceService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<CustomResponse<List<AdditionalServiceDto>>> GetAditionalServicesAsync()
        {
            var data = await _cacheService.GetOrCreateAsync("additionalServices", async () =>
            {
                var additionalServiceQuery = await _unitOfWork.GetQueryableAsync<AdditionalService>();

                var additionalServices = await additionalServiceQuery.ToListAsync();

                return _mapper.Map<List<AdditionalServiceDto>>(additionalServices);
            });

            return new CustomResponse<List<AdditionalServiceDto>>
            {
                Data = data
            };
        }
    }
}
