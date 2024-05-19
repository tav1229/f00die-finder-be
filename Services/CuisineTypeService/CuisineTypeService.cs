using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.CuisineTypeService
{
    public class CuisineTypeService : BaseService, ICuisineTypeService
    {
        public CuisineTypeService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<CustomResponse<List<CuisineTypeDto>>> GetCuisineTypesAsync()
        {
            var data = await _cacheService.GetOrCreateAsync("cuisineTypes", async () =>
            {
                var cuisineTypeQuery = await _unitOfWork.GetQueryableAsync<CuisineType>();
                var cuisineTypes = await cuisineTypeQuery.ToListAsync();

                return _mapper.Map<List<CuisineTypeDto>>(cuisineTypes);
            });

            return new CustomResponse<List<CuisineTypeDto>>
            {
                Data = data
            };
        }
    }
}
