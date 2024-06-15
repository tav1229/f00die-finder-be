using f00die_finder_be.Common;

namespace f00die_finder_be.Dtos.Reservation
{
    public class FilterReservationDto
    {
        public ReservationStatus? ReservationStatus { get; set; }
        public DateTimeOffset? ReservationTime { get; set; }
    }
}
