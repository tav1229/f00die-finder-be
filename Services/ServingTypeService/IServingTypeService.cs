using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Restaurant;

namespace f00die_finder_be.Services.ServingTypeService
{
    public interface IServingTypeService
    {
        Task<CustomResponse<List<ServingTypeDto>>> GetServingTypesAsync();
    }
}
