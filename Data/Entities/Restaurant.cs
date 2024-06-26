using f00die_finder_be.Common;

namespace f00die_finder_be.Data.Entities
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }

        public string? Phone { get; set; }

        public RestaurantStatus Status { get; set; }

        public Guid PriceRangePerPersonId { get; set; }
        public PriceRangePerPerson PriceRangePerPerson { get; set; }

        public int Capacity { get; set; }

        public int ReservationCount { get; set; }
        public short Rating { get; set; }

        public string? SpecialDishes { get; set; }

        public string? Description { get; set; }

        public string? Note { get; set; }

        public User Owner { get; set; }
        public Guid OwnerId { get; set; }

        public Location? Location { get; set; }

        public List<BusinessHour>? BusinessHours { get; set; }

        public List<RestaurantImage>? Images { get; set; }

        public List<RestaurantCustomerType>? RestaurantCustomerTypes { get; set; }

        public List<RestaurantCuisineType>? RestaurantCuisineTypes { get; set; }

        public List<RestaurantServingType>? RestaurantServingTypes { get; set; }

        public List<RestaurantAdditionalService>? RestaurantAdditionalServices { get; set; }

        public List<Reservation>? Reservations { get; set; }

        public List<ReviewComment>? Reviews { get; set; }

    }
}
