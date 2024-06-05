using f00die_finder_be.Common;

namespace f00die_finder_be.Dtos.Reservation
{
    public class ReservationOwnerDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
    }
}
