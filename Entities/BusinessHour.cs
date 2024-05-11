namespace f00die_finder_be.Entities
{
    public class BusinessHour : BaseEntity
    {
        public DayOfWeek DayOfWeek { get; set; }
        
        public TimeSpan OpenTime { get; set; }
        
        public TimeSpan CloseTime { get; set; }
                
        public Restaurant Restaurant { get; set; }

        public Guid RestaurantId { get; set; }
    }
}