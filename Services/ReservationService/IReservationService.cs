using f00die_finder_be.Common;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Reservation;

namespace f00die_finder_be.Services.ReservationService
{
    public interface IReservationService
    {
        Task<PagedResult<ReservationDto>> GetReservationsOfRestaurantAsync(Guid restaurantId, ReservationStatus? reservationStatus, DateTime? time, int pageSize, int pageNumber);
        Task<Guid> AddAsync(ReservationAddDto reservationAddDto);
        Task<ReservationDetailDto> GetReservationByIdAsync(Guid reservationId);
        Task UpdateReservationStatusAsync(Guid reservationId, ReservationStatus reservationStatus);
    }
}