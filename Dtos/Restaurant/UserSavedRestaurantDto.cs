using f00die_finder_be.Common;
using f00die_finder_be.Dtos.Location;

namespace f00die_finder_be.Dtos.Restaurant
{
    public class UserSavedRestaurantDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public LocationDto Location { get; set; }

        public WardOrCommuneDto WardOrCommune { get; set; }

        public DistrictDto District { get; set; }

        public ProvinceOrCityDto ProvinceOrCity { get; set; }

        public PriceRangePerPerson PriceRangePerPerson { get; set; }

        public List<string> Images { get; set; }
        public short Rating { get; set; }
    }
}
