using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Restaurant;

namespace f00die_finder_be.Services.AdditionalServiceService
{
    public interface IAdditionalServiceService
    {
        Task<CustomResponse<List<AdditionalServiceDto>>> GetAditionalServicesAsync();
    }
}
