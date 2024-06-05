using f00die_finder_be.Common;
using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos.Location;

namespace f00die_finder_be.Dtos.Restaurant
{
    public class RestaurantDetailDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }

        public LocationDto Location { get; set; }

        public WardOrCommuneDto WardOrCommune { get; set; }

        public DistrictDto District { get; set; }

        public ProvinceOrCityDto ProvinceOrCity { get; set; }

        public List<CuisineTypeDto> CuisineTypes { get; set; }

        public List<ServingTypeDto> ServingTypes { get; set; }

        public List<CustomerTypeDto> CustomerTypes { get; set; }

        public PriceRangePerPerson PriceRangePerPerson { get; set; }

        public int Capacity { get; set; }

        public string SpecialDishes { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public List<ImageDto>? RestaurantImages { get; set; }

        public List<ImageDto>? MenuImages { get; set; }

        public List<BusinessHourDto>? BusinessHours { get; set; }

        public List<AdditionalServiceDto>? AdditionalServices { get; set; }

        public RestaurantStatus? Status { get; set; }
        
        public short Rating { get; set; }
    }
}