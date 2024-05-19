using f00die_finder_be.Common;

namespace f00die_finder_be.Dtos.Reservation
{
    public class ReservationDetailDto
    {
        public Guid Id { get; set; }

        public DateTimeOffset Time { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }
        
        public int NumberOfAdults { get; set; }
        
        public int NumberOfChildren { get; set; }
        
        public string? Note { get; set; }
        
        public DateTimeOffset CreatedDate { get; set; }

        public ReservationStatus ReservationStatus { get; set; }
    }
}