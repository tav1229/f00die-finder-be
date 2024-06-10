using f00die_finder_be.Common;

namespace f00die_finder_be.Dtos.Restaurant
{
    public class RestaurantAdminDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ProvinceOrCity { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public RestaurantStatus Status { get; set; }
    }
}
