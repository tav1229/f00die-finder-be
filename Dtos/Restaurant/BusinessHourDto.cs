namespace f00die_finder_be.Dtos.Restaurant
{
    public class BusinessHourDto
    {
        public Guid Id { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeSpan OpenTime { get; set; }

        public TimeSpan CloseTime { get; set; }
    }
}