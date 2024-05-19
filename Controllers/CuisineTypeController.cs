using f00die_finder_be.Filters;
using f00die_finder_be.Services.CuisineTypeService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers
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
    }
}
