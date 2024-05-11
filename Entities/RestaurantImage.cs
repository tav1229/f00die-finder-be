using f00die_finder_be.Common;

namespace f00die_finder_be.Entities
{
    public class RestaurantImage : BaseEntity
    {        
        public string URL { get; set; }
        
        public ImageType ImageType { get; set; }

        public Restaurant Restaurant { get; set; }
        
        public Guid RestaurantId { get; set; }
    }
}