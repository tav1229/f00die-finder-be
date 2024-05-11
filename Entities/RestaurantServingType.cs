namespace f00die_finder_be.Entities
{
    public class RestaurantServingType : BaseEntity
    {     
        public Restaurant Restaurant { get; set; }
        public Guid RestaurantId { get; set; }
        
        public ServingType ServingType { get; set; }
        public Guid ServingTypeId { get; set; }
    }
}