using f00die_finder_be.Common;
using f00die_finder_be.Filters;
using f00die_finder_be.Services.RestaurantOwnerDashboardService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeFilterAttribute([Role.RestaurantOwner])]
    public class RestaurantOwnerDashboardController : ControllerBase
    {
        private readonly IRestaurantOwnerDashboardService _restaurantOwnerDashboardService;

        public RestaurantOwnerDashboardController(IRestaurantOwnerDashboardService restaurantOwnerDashboardService)
        {
            _restaurantOwnerDashboardService = restaurantOwnerDashboardService;
        }

        [HttpGet("total-reservations")]
        public async Task<IActionResult> GetTotalReservationsAsync()
        {
            var response = await _restaurantOwnerDashboardService.GetTotalReservationsAsync();
            return Ok(response);
        }

        [HttpGet("reservations-by-month")]
        public async Task<IActionResult> GetReservationsByMonthAsync()
        {
            var response = await _restaurantOwnerDashboardService.GetReservationsByMonth();
            return Ok(response);
        }

        [HttpGet("distinct-user-reservation-count")]
        public async Task<IActionResult> GetDistinctUserReservationCountAsync()
        {
            var response = await _restaurantOwnerDashboardService.GetDistinctUserReservationCount();
            return Ok(response);
        }
    }
}
