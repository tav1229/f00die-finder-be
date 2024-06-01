using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Restaurant;

namespace f00die_finder_be.Services.PriceRangePerPersonService
{
    public interface IPriceRangePerPersonService
    {
        Task<CustomResponse<List<PriceRangePerPersonDto>>> GetPriceRangePerPersonsAsync();
    }
}
