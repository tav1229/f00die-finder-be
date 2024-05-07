using f00die_finder_be.Filters;
using f00die_finder_be.Services.CuisineTypeService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers.CuisineType
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeFilter]
    public class CuisineTypeController : ControllerBase
    {
        private readonly ICuisineTypeService _cuisineTypeService;

        public CuisineTypeController(ICuisineTypeService cuisineTypeService)
        {
            _cuisineTypeService = cuisineTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCuisineTypesAsync()
        {
            var result = await _cuisineTypeService.GetCuisineTypesAsync();
            return Ok(result);
        }

        [HttpGet("restaurant/{restaurantId}")]
        public async Task<IActionResult> GetCuisineTypesByRestaurantAsync(Guid restaurantId)
        {
            var result = await _cuisineTypeService.GetCuisineTypesByRestaurantAsync(restaurantId);
            return Ok(result);
        }
    }
}
