using f00die_finder_be.Common;
using f00die_finder_be.Dtos.Location;

namespace f00die_finder_be.Dtos.Restaurant
{
    public class RestaurantDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public LocationDto Location { get; set; }

        public WardOrCommuneDto Ward { get; set; }

        public DistrictDto District { get; set; }

        public ProvinceOrCityDto City { get; set; }

        public List<CuisineTypeDto> CuisineTypes { get; set; }

        public List<ServingTypeDto> ServingTypes { get; set; }

        public PriceRangePerPerson PriceRangePerPerson { get; set; }

        public string ImageUrl { get; set; }
    }
}