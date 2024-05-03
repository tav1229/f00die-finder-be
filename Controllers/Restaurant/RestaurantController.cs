using f00die_finder_be.Dtos.Restaurant;
using f00die_finder_be.Filters;
using f00die_finder_be.Services.RestaurantService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers.Restaurant
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeFilter]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRestaurantsAsync([FromQuery] FilterRestaurantDto? filterRestaurantDto, [FromQuery] string searchValue, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            var result = await _restaurantService.GetRestaurantsAsync(filterRestaurantDto, searchValue, pageSize, pageNumber);
            return Ok(result);
        }

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetRestaurantByIdAsync(Guid restaurantId)
        {
            var result = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
            return Ok(result);
        }

        [HttpGet("owner/{userId}")]
        public async Task<IActionResult> GetRestaurantByOwnerId(Guid userId)
        {
            var result = await _restaurantService.GetRestaurantByOwnerId(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] RestaurantAddDto restaurant)
        {
            var result = await _restaurantService.AddAsync(restaurant);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] RestaurantUpdateDto restaurant)
        {
            await _restaurantService.UpdateAsync(restaurant);
            return Ok();
        }

        [HttpDelete("{restaurantId}")]
        public async Task<IActionResult> DeleteAsync(Guid restaurantId)
        {
            await _restaurantService.DeleteAsync(restaurantId);
            return Ok();
        }
    }
}
