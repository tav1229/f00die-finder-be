using f00die_finder_be.Dtos.Location;

namespace f00die_finder_be.Dtos.Restaurant
{
    public class RestaurantDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public LocationDto Location { get; set; }

        public WardOrCommuneDto WardOrCommune { get; set; }

        public DistrictDto District { get; set; }

        public ProvinceOrCityDto ProvinceOrCity { get; set; }

        public List<CuisineTypeDto> CuisineTypes { get; set; }

        public List<ServingTypeDto> ServingTypes { get; set; }

        public List<CustomerTypeDto> CustomerTypes { get; set; }

        public PriceRangePerPersonDto PriceRangePerPerson { get; set; }

        public List<string> Images { get; set; }

        public short Rating { get; set; }
    }
}