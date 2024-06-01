using f00die_finder_be.Services.ServingTypeService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
