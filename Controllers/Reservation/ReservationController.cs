using f00die_finder_be.Common;
using f00die_finder_be.Dtos.Reservation;
using f00die_finder_be.Filters;
using f00die_finder_be.Services.ReservationService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers.Reservation
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeFilter]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("restaurant/{restaurantId}")]
        public async Task<IActionResult> GetReservationsOfRestaurantAsync(Guid restaurantId, ReservationStatus? reservationStatus, DateTime? time, int pageSize = 10, int pageNumber = 1)
        {
            var result = await _reservationService.GetReservationsOfRestaurantAsync(restaurantId, reservationStatus, time, pageSize, pageNumber);
            return Ok(result);
        }

        [HttpGet("{reservationId}")]
        public async Task<IActionResult> GetReservationByIdAsync(Guid reservationId)
        {
            var result = await _reservationService.GetReservationByIdAsync(reservationId);
            return Ok(result);
        }

        [HttpPost("restaurant")]
        public async Task<IActionResult> AddAsync([FromBody] ReservationAddDto reservationAddDto)
        {
            var result = await _reservationService.AddAsync(reservationAddDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReservationStatusAsync(Guid reservationId, ReservationStatus reservationStatus)
        {
            await _reservationService.UpdateReservationStatusAsync(reservationId, reservationStatus);
            return Ok();
        }

    }
}
