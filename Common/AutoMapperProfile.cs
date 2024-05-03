using AutoMapper;
using f00die_finder_be.Dtos.Location;
using f00die_finder_be.Dtos.Restaurant;
using f00die_finder_be.Models;

namespace f00die_finder_be.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BusinessHour, BusinessHourDto>();
            CreateMap<Location, LocationDto>();
            CreateMap<WardOrCommune, WardOrCommuneDto>();
            CreateMap<District, DistrictDto>();
            CreateMap<ProvinceOrCity, ProvinceOrCityDto>();
            CreateMap<CuisineType, CuisineTypeDto>();
            CreateMap<ServingType, ServingTypeDto>();
            CreateMap<RestaurantImage, ImageDto>();
            CreateMap<AdditionalService, AdditionalServiceDto>();
            CreateMap<Restaurant, RestaurantDetailDto>();
            CreateMap<Restaurant, RestaurantDto>();
        }
    }
}
