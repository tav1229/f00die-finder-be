using f00die_finder_be.Services.AdditionalServiceService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalServiceController : ControllerBase
    {
        private readonly IAdditionalServiceService _additionalServiceService;

        public AdditionalServiceController(IAdditionalServiceService additionalServiceService)
        {
            _additionalServiceService = additionalServiceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdditionalServicesAsync()
        {
            var result = await _additionalServiceService.GetAditionalServicesAsync();
            return Ok(result);
        }
    }
}
