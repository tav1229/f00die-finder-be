namespace f00die_finder_be.Entities
{
    public class RestaurantCuisineType : BaseEntity
    {
        public Restaurant Restaurant { get; set; }

        public Guid RestaurantId { get; set; } 
        public CuisineType CuisineType { get; set; }
        public Guid CuisineTypeId { get; set; }

        
    }
}