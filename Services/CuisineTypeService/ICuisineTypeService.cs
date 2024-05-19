using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Restaurant;

namespace f00die_finder_be.Services.CuisineTypeService
{
    public interface ICuisineTypeService
    {
        Task<CustomResponse<List<CuisineTypeDto>>> GetCuisineTypesAsync();
    }
}
