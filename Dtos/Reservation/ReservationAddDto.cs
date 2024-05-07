namespace f00die_finder_be.Dtos.Reservation
{
    public class ReservationAddDto
    {
        public Guid RestaurantId { get; set; }
        public DateTime Time { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public int NumberOfAdults { get; set; }

        public int NumberOfChildren { get; set; }

        public string? Note { get; set; }
    }
}
