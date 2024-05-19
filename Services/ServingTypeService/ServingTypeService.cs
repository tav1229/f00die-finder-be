using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.ServingTypeService
{
    public class ServingTypeService : BaseService, IServingTypeService
    {
        public ServingTypeService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<CustomResponse<List<ServingTypeDto>>> GetServingTypesAsync()
        {
            var data = await _cacheService.GetOrCreateAsync("servingTypes", async () =>
            {
                var servingTypeQuery = await _unitOfWork.GetQueryableAsync<ServingType>();
                var servingTypes = await servingTypeQuery.ToListAsync();

                return _mapper.Map<List<ServingTypeDto>>(servingTypes);
            });

            return new CustomResponse<List<ServingTypeDto>>
            {
                Data = data
            };
        }
    }
}
