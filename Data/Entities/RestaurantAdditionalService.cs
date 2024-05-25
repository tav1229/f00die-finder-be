namespace f00die_finder_be.Data.Entities
{
    public class RestaurantAdditionalService : BaseEntity
    {
        public AdditionalService AdditionalService { get; set; }
        public Guid AdditionalServiceId { get; set; }

        public Restaurant Restaurant { get; set; }

        public Guid RestaurantId { get; set; }

    }
}