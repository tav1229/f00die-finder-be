namespace f00die_finder_be.Dtos.Restaurant
{
    public class AddBusinessHourDto
    {
        public short Date { get; set; }

        public string OpenTime { get; set; }

        public string CloseTime { get; set; }
    }
}