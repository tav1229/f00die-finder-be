using f00die_finder_be.Common;

namespace f00die_finder_be.Dtos.Reservation
{
    public class ReservationCustomerDto
    {
        public Guid Id { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantImageUrl { get; set; }
        public DateTimeOffset Time { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}