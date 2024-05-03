using f00die_finder_be.Common;
using f00die_finder_be.Dtos.Location;
using System.ComponentModel.DataAnnotations;

namespace f00die_finder_be.Dtos.Restaurant
{
    public class RestaurantDetailDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public LocationDto Location { get; set; }

        public WardOrCommuneDto WardOrCommuneDto { get; set; }

        public DistrictDto District { get; set; }

        public ProvinceOrCityDto ProvinceOrCity { get; set; }

        public List<CuisineTypeDto> CuisineTypes { get; set; }

        public List<ServingTypeDto> ServingTypes { get; set; }

        public PriceRangePerPerson PriceRangePerPerson { get; set; }

        public int Capacity { get; set; }

        public string SpecialDishes { get; set; }

        public string Introduction { get; set; }

        public string Note { get; set; }

        public List<ImageDto>? RestaurantImages { get; set; }

        public List<ImageDto>? MenuImages { get; set; }

        public List<BusinessHourDto>? BusinessHours { get; set; }

        public List<AdditionalServiceDto>? AdditionalServices { get; set; }

        public RestaurantStatus? RestaurantStatus { get; set; }
    }
}