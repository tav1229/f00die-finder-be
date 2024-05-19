namespace f00die_finder_be.Data.Entities
{
    public class Location : BaseEntity
    {
        public string? Address { get; set; }

        public WardOrCommune? WardOrCommune { get; set; }
        public Guid? WardOrCommuneId { get; set; }

        public Restaurant? Restaurant { get; set; }
        public Guid? RestaurantId { get; set; }

    }
}