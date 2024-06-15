using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.PriceRangePerPersonService
{
    public class PriceRangePerPersonService : BaseService, IPriceRangePerPersonService
    {
        public PriceRangePerPersonService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<CustomResponse<List<PriceRangePerPersonDto>>> GetPriceRangePerPersonsAsync()
        {
            var data = await _cacheService.GetOrCreateAsync("priceRangePerPersons", async () =>
            {
                var priceRangePerPersonQuery = await _unitOfWork.GetQueryableAsync<PriceRangePerPerson>();
                var priceRangePerPersons = await priceRangePerPersonQuery.ToListAsync();

                return _mapper.Map<List<PriceRangePerPersonDto>>(priceRangePerPersons);
            });

            return new CustomResponse<List<PriceRangePerPersonDto>>
            {
                Data = data
            };
        }
    }
}
