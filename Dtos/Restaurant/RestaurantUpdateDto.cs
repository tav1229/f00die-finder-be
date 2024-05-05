using f00die_finder_be.Common;

namespace f00die_finder_be.Dtos.Restaurant
{
    public class RestaurantUpdateDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public string? Phone { get; set; }

        public PriceRangePerPerson? PriceRangePerPerson { get; set; }

        public int? Capacity { get; set; }

        public string? SpecialDishes { get; set; }

        public string? Description { get; set; }

        public string? Note { get; set; }

        public List<IFormFile>? RestaurantImages { get; set; }

        public List<Guid>? CuisineTypes { get; set; }

        public List<Guid>? ServingTypes { get; set; }

        public string? Address { get; set; }

        public Guid? Ward { get; set; }

        public List<Guid>? AdditionalServices { get; set; }

        public List<AddBusinessHourDto>? BusinessHours { get; set; }

        public List<IFormFile>? MenuImages { get; set; }

    }
}
