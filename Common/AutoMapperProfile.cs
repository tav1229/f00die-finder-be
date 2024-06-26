﻿using AutoMapper;
using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos.Location;
using f00die_finder_be.Dtos.Reservation;
using f00die_finder_be.Dtos.Restaurant;
using f00die_finder_be.Dtos.ReviewComment;
using f00die_finder_be.Dtos.User;

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
            CreateMap<AdditionalService, AdditionalServiceDto>();
            CreateMap<CustomerType, CustomerTypeDto>();
            CreateMap<PriceRangePerPerson, PriceRangePerPersonDto>();

            CreateMap<RestaurantImage, ImageDto>();
            CreateMap<Restaurant, RestaurantDetailDto>()
                .ForMember(dest => dest.WardOrCommune, opt => opt.MapFrom(src => src.Location.WardOrCommune))
                .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.Location.WardOrCommune.District))
                .ForMember(dest => dest.ProvinceOrCity, opt => opt.MapFrom(src => src.Location.WardOrCommune.District.ProvinceOrCity))
                .ForMember(dest => dest.RestaurantImages, opt => opt.MapFrom(src => src.Images.Where(i => i.ImageType == ImageType.Restaurant)))
                .ForMember(dest => dest.MenuImages, opt => opt.MapFrom(src => src.Images.Where(i => i.ImageType == ImageType.Menu)))
                .ForMember(dest => dest.BusinessHours, opt => opt.MapFrom(src => src.BusinessHours))
                .ForMember(dest => dest.AdditionalServices, opt => opt.MapFrom(src => src.RestaurantAdditionalServices.Select(ras => ras.AdditionalService)))
                .ForMember(dest => dest.CuisineTypes, opt => opt.MapFrom(src => src.RestaurantCuisineTypes.Select(rct => rct.CuisineType)))
                .ForMember(dest => dest.ServingTypes, opt => opt.MapFrom(src => src.RestaurantServingTypes.Select(rst => rst.ServingType)))
                .ForMember(dest => dest.CustomerTypes, opt => opt.MapFrom(src => src.RestaurantCustomerTypes.Select(rct => rct.CustomerType)));
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(dest => dest.WardOrCommune, opt => opt.MapFrom(src => src.Location.WardOrCommune))
                .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.Location.WardOrCommune.District))
                .ForMember(dest => dest.ProvinceOrCity, opt => opt.MapFrom(src => src.Location.WardOrCommune.District.ProvinceOrCity))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Where(i => i.ImageType == ImageType.Restaurant).Select(i => i.URL)))
                .ForMember(dest => dest.CuisineTypes, opt => opt.MapFrom(src => src.RestaurantCuisineTypes.Select(rct => rct.CuisineType)))
                .ForMember(dest => dest.ServingTypes, opt => opt.MapFrom(src => src.RestaurantServingTypes.Select(rst => rst.ServingType)))
                .ForMember(dest => dest.CustomerTypes, opt => opt.MapFrom(src => src.RestaurantCustomerTypes.Select(rct => rct.CustomerType)));
            CreateMap<Restaurant, UserSavedRestaurantDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Images.Where(i => i.ImageType == ImageType.Restaurant).Select(i => i.URL).FirstOrDefault()))
                .ForMember(dest => dest.WardOrCommune, opt => opt.MapFrom(src => src.Location.WardOrCommune));
            CreateMap<Restaurant, RestaurantAdminDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Location.Address))
                .ForMember(dest => dest.ProvinceOrCity, opt => opt.MapFrom(src => src.Location.WardOrCommune.District.ProvinceOrCity.Name));

            CreateMap<Reservation, ReservationCustomerDto>()
                .ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.Restaurant.Name))
                .ForMember(dest => dest.RestaurantImageUrl, opt => opt.MapFrom(src => src.Restaurant.Images.FirstOrDefault(i => i.ImageType == ImageType.Restaurant).URL))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.ReservationTime));
            CreateMap<Reservation, ReservationDetailDto>()
                .ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.Restaurant.Name))
                .ForMember(dest => dest.RestaurantImageUrl, opt => opt.MapFrom(src => src.Restaurant.Images.FirstOrDefault(i => i.ImageType == ImageType.Restaurant).URL))
                .ForMember(dest => dest.RestaurantAddress, opt => opt.MapFrom(src => src.Restaurant.Location.WardOrCommune.Name + ", " + src.Restaurant.Location.WardOrCommune.District.Name))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.ReservationTime));
            CreateMap<ReservationAddDto, Reservation>()
                .ForMember(dest => dest.ReservationTime, opt => opt.MapFrom(src => src.Time));
            CreateMap<Reservation, ReservationOwnerDto>()
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.ReservationTime));

            CreateMap<ReviewComment, ReviewCommentDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName));
            CreateMap<ReviewCommentAddDto, ReviewComment>();

            CreateMap<User, UserDetailDto>();
            CreateMap<User, UserAdminDto>();
        }
    }
}
