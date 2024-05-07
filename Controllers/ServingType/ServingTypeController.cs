using f00die_finder_be.Filters;
using f00die_finder_be.Services.ServingTypeService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers.ServingType
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeFilter]
    public class ServingTypeController : ControllerBase
    {
        private readonly IServingTypeService _servingTypeService;

        public ServingTypeController(IServingTypeService servingTypeService)
        {
            _servingTypeService = servingTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetServingTypesAsync()
        {
            var result = await _servingTypeService.GetServingTypesAsync();
            return Ok(result);
        }

        [HttpGet("restaurant/{restaurantId}")]
        public async Task<IActionResult> GetServingTypesByRestaurantAsync(Guid restaurantId)
        {
            var result = await _servingTypeService.GetServingTypesByRestaurantAsync(restaurantId);
            return Ok(result);
        }
    }
}
