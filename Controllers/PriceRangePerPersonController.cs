using f00die_finder_be.Services.PriceRangePerPersonService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceRangePerPersonController : ControllerBase
    {
        private readonly IPriceRangePerPersonService _priceRangePerPersonService;

        public PriceRangePerPersonController(IPriceRangePerPersonService priceRangePerPersonService)
        {
            _priceRangePerPersonService = priceRangePerPersonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPriceRangePerPersonsAsync()
        {
            var result = await _priceRangePerPersonService.GetPriceRangePerPersonsAsync();
            return Ok(result);
        }
    }
}
