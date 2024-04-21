using f00die_finder_be.Common;

namespace f00die_finder_be.Models
{
    public class Reservation : BaseEntity
    {
        public DateTime Time { get; set; }
        
        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public int NumberOfAdults { get; set; }

        public int NumberOfChildren { get; set; }

        public string? Note { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }

        public Restaurant Restaurant { get; set; }

        public Guid RestaurantId { get; set; }

        public ReservationStatus ReservationStatus { get; set; }

    }
}
