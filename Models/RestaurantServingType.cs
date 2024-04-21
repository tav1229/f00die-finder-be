using f00die_finder_be.Common;

namespace f00die_finder_be.Models
{
    public class RestaurantServingType : BaseEntity
    {     
        public Restaurant Restaurant { get; set; }
        public Guid RestaurantId { get; set; }
        
        public ServingType ServingType { get; set; }
        public Guid ServingTypeId { get; set; }
    }
}