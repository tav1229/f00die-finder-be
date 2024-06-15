namespace f00die_finder_be.Dtos.Restaurant
{
    public class FilterRestaurantDto
    {
        public Guid? ProvinceOrCityId { get; set; }

        public Guid? DistrictId { get; set; }

        public Guid? WardOrCommuneId { get; set; }

        public Guid? PriceRangePerPerson { get; set; }

        public Guid? CuisineType { get; set; }

        public Guid? ServingType { get; set; }

        public Guid? CustomerType { get; set; }
        public string? SearchValue { get; set; }
    }
}