using f00die_finder_be.Common;

namespace f00die_finder_be.Models
{
    public class Location : BaseEntity
    {
        public string? Address { get; set; }
                
        public WardOrCommune? Ward { get; set; }
        public Guid? WardId { get; set; }

        public Restaurant? Restaurant { get; set; }
        public Guid? RestaurantId { get; set; }

    }
}