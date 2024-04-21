using f00die_finder_be.Common;

namespace f00die_finder_be.Models
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }

        public string? Phone { get; set; }

        public RestaurantStatus Status { get; set; }

        public PriceRangePerPerson PriceRange { get; set; }

        public int Capacity { get; set; }

        public int ReservationCount { get; set; }

        public string? SpecialDishes { get; set; }

        public string? Introduction { get; set; }

        public string? Note { get; set; }

        public User Owner { get; set; }
        public Guid OwnerId { get; set; }

        public Location? Location { get; set; }

        public List<BusinessHour>? BusinessHours { get; set; }

        public List<RestaurantImage>? Images { get; set; }

        public List<RestaurantCuisineType>? RestaurantCuisineTypes { get; set; }

        public List<RestaurantServingType>? RestaurantServingTypes { get; set; }

        public List<RestaurantAdditionalService>? RestaurantAdditionalServices { get; set; }

        public List<Reservation>? Reservations { get; set; }

        public List<ReviewComment>? Reviews { get; set; }

    }
}
