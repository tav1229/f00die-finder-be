using f00die_finder_be.Common;
using f00die_finder_be.Dtos.Restaurant;
using f00die_finder_be.Filters;
using f00die_finder_be.Services.RestaurantService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers
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
        public async Task<IActionResult> GetRestaurantsAsync([FromQuery] FilterRestaurantDto? filterRestaurantDto,
            [FromQuery] RestaurantSortType sortType, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var result = await _restaurantService.GetRestaurantsAsync(filterRestaurantDto, sortType, pageSize, pageNumber);
            return Ok(result);
        }

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetRestaurantByIdAsync(Guid restaurantId)
        {
            var result = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
            return Ok(result);
        }

        [HttpGet("my-restaurant")]
        public async Task<IActionResult> GetMyRestaurantAsync()
        {
            var result = await _restaurantService.GetMyRestaurantAsync();
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

        [HttpPost("images")]
        public async Task<IActionResult> AddImagesAsync([FromForm] RestaurantAddImagesDto restaurant)
        {
            await _restaurantService.AddImagesAsync(restaurant);
            return Ok();
        }

        [HttpDelete("images")]
        public async Task<IActionResult> DeleteImagesAsync([FromBody] List<Guid> imageIds)
        {
            await _restaurantService.DeleteImagesAsync(imageIds);
            return Ok();
        }

        [HttpPut("deactivate")]
        public async Task<IActionResult> DeactivateAsync()
        {
            await _restaurantService.DeactivateAsync();
            return Ok();
        }
    }
}
