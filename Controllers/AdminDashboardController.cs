using f00die_finder_be.Common;
using f00die_finder_be.Filters;
using f00die_finder_be.Services.AdminDashboardService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers
{
    [Route("api/admin-dashboard")]
    [ApiController]
    [AuthorizeFilterAttribute([Role.Admin])]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminDashboardService _adminDashboardService;

        public AdminDashboardController(IAdminDashboardService adminDashboardService)
        {
            _adminDashboardService = adminDashboardService;
        }

        [HttpGet("reservations-by-month")]
        public async Task<IActionResult> GetReservationsByMonthAsync()
        {
            var response = await _adminDashboardService.GetReservationsByMonth();
            return Ok(response);
        }

        [HttpGet("total-restaurants")]
        public async Task<IActionResult> GetTotalRestaurantsAsync()
        {
            var response = await _adminDashboardService.GetTotalRestaurantsAsync();
            return Ok(response);
        }

        [HttpGet("total-users")]
        public async Task<IActionResult> GetTotalUsersAsync()
        {
            var response = await _adminDashboardService.GetTotalUsersAsync();
            return Ok(response);
        }

        [HttpGet("total-reservations")]
        public async Task<IActionResult> GetTotalReservationsAsync()
        {
            var response = await _adminDashboardService.GetTotalReservationsAsync();
            return Ok(response);
        }
    }
}
