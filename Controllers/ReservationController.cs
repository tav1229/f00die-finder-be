using f00die_finder_be.Common;
using f00die_finder_be.Dtos.Reservation;
using f00die_finder_be.Filters;
using f00die_finder_be.Services.ReservationService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [AuthorizeFilter([Role.RestaurantOwner])]
        [HttpGet("my-restaurant")]
        public async Task<IActionResult> GetReservationsOfMyRestaurantAsync([FromQuery] FilterReservationDto filter, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var result = await _reservationService.GetReservationsOfMyRestaurantAsync(filter, pageSize, pageNumber);
            return Ok(result);
        }

        [AuthorizeFilter([Role.Customer, Role.RestaurantOwner])]
        [HttpGet("{reservationId}")]
        public async Task<IActionResult> GetReservationByIdAsync(Guid reservationId)
        {
            var result = await _reservationService.GetReservationByIdAsync(reservationId);
            return Ok(result);
        }

        [AuthorizeFilter([Role.Customer])]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ReservationAddDto reservationAddDto)
        {
            var result = await _reservationService.AddAsync(reservationAddDto);
            return Ok(result);
        }

        [AuthorizeFilter([Role.Customer, Role.RestaurantOwner])]
        [HttpPut]
        public async Task<IActionResult> UpdateReservationStatusAsync(Guid reservationId, ReservationStatus reservationStatus)
        {
            var result = await _reservationService.UpdateReservationStatusAsync(reservationId, reservationStatus);
            return Ok(result);
        }

        [AuthorizeFilter([Role.Customer])]
        [HttpGet("my-reservations")]
        public async Task<IActionResult> GetMyReservations([FromQuery] FilterReservationDto filter, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var result = await _reservationService.GetMyReservations(filter, pageSize, pageNumber);
            return Ok(result);
        }
    }
}
