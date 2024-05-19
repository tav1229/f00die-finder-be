using f00die_finder_be.Common;

namespace f00die_finder_be.Dtos.Reservation
{
    public class FilterReservationsOfRestaurantDto
    {
        public Guid RestaurantId { get; set; }
        public ReservationStatus? ReservationStatus { get; set; }
        public DateTimeOffset? ReservationTime { get; set; }
    }
}
