using f00die_finder_be.Filters;
using f00die_finder_be.Services.CustomerTypeService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers.CustomerType
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeFilter]
    public class CustomerTypeController : ControllerBase
    {
        private readonly ICustomerTypeService _customerTypeService;

        public CustomerTypeController(ICustomerTypeService customerTypeService)
        {
            _customerTypeService = customerTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerTypesAsync()
        {
            var result = await _customerTypeService.GetCustomerTypesAsync();
            return Ok(result);
        }

        [HttpGet("restaurant/{restaurantId}")]
        public async Task<IActionResult> GetCustomerTypesByRestaurantAsync(Guid restaurantId)
        {
            var result = await _customerTypeService.GetCustomerTypesByRestaurantAsync(restaurantId);
            return Ok(result);
        }
    }
}
