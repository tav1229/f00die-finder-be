﻿using f00die_finder_be.Common;
using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.RestaurantService
{
    public class RestaurantService : BaseService, IRestaurantService
    {
        public RestaurantService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<CustomResponse<RestaurantDetailDto>> AddAsync(RestaurantAddDto restaurantDto)
        {
            var cuisineTypes = new List<RestaurantCuisineType>();
            if (restaurantDto.CuisineTypes != null)
            {
                foreach (var cuisineType in restaurantDto.CuisineTypes)
                {
                    cuisineTypes.Add(new RestaurantCuisineType()
                    {
                        CuisineTypeId = cuisineType
                    });
                }
            }

            var serviceTypes = new List<RestaurantServingType>();
            if (restaurantDto.ServingTypes != null)
            {
                foreach (var serviceType in restaurantDto.ServingTypes)
                {
                    serviceTypes.Add(new RestaurantServingType()
                    {
                        ServingTypeId = serviceType
                    });
                }
            }

            var customerTypes = new List<RestaurantCustomerType>();
            if (restaurantDto.CustomerTypes != null)
            {
                foreach (var customerType in restaurantDto.CustomerTypes)
                {
                    customerTypes.Add(new RestaurantCustomerType()
                    {
                        CustomerTypeId = customerType
                    });
                }
            }

            var additionalServices = new List<RestaurantAdditionalService>();
            if (restaurantDto.AdditionalServices != null)
            {
                foreach (var additionalService in restaurantDto.AdditionalServices)
                {
                    additionalServices.Add(new RestaurantAdditionalService()
                    {
                        AdditionalServiceId = additionalService
                    });
                }
            }

            var businessHours = new List<BusinessHour>();
            if (restaurantDto.BusinessHours != null)
            {
                foreach (var businessHour in restaurantDto.BusinessHours)
                {
                    businessHours.Add(new BusinessHour()
                    {
                        DayOfWeek = (DayOfWeek)businessHour.Date,
                        OpenTime = TimeSpan.Parse(businessHour.OpenTime),
                        CloseTime = TimeSpan.Parse(businessHour.CloseTime)
                    });
                }
            }
            var restaurant = new Restaurant()
            {
                Name = restaurantDto.Name,
                Phone = restaurantDto.Phone,
                PriceRangePerPerson = restaurantDto.PriceRangePerPerson,
                Capacity = restaurantDto.Capacity,
                SpecialDishes = restaurantDto.SpecialDishes,
                Description = restaurantDto.Description,
                Note = restaurantDto.Note,
                OwnerId = _currentUserService.UserId,
                RestaurantCuisineTypes = cuisineTypes,
                RestaurantServingTypes = serviceTypes,
                RestaurantCustomerTypes = customerTypes,
                Location = new f00die_finder_be.Data.Entities.Location()
                {
                    Address = restaurantDto.Address,
                    WardOrCommuneId = restaurantDto.Ward
                },
                RestaurantAdditionalServices = additionalServices,
                BusinessHours = businessHours,
                Status = RestaurantStatus.Pending
            };

            await _unitOfWork.AddAsync(restaurant);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveAsync("restaurants");

            return new CustomResponse<RestaurantDetailDto>
            {
                Data = _mapper.Map<RestaurantDetailDto>(restaurant)
            };
        }

        public async Task<CustomResponse<RestaurantDetailDto>> DeactivateAsync()
        {
            var restaurantQuery = await _unitOfWork.GetQueryableAsync<Restaurant>();
            var restaurant = await restaurantQuery.FirstOrDefaultAsync(r => r.OwnerId == _currentUserService.UserId);
            if (restaurant == null)
            {
                throw new NotFoundException();
            }
            restaurant.Status = RestaurantStatus.Inactive;

            await _unitOfWork.UpdateAsync(restaurant);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveAsync($"restaurant-{restaurant.Id}");
            await _cacheService.RemoveAsync($"restaurants");
            await _cacheService.RemoveAsync($"restaurant-owner-{restaurant.OwnerId}");

            return new CustomResponse<RestaurantDetailDto>
            {
                Data = _mapper.Map<RestaurantDetailDto>(restaurant)
            };
        }

        public async Task<CustomResponse<RestaurantDetailDto>> GetRestaurantByIdAsync(Guid restaurantId)
        {
            var data = await _cacheService.GetOrCreateAsync($"restaurant-{restaurantId}", async () =>
            {
                var restaurantQuery = await _unitOfWork.GetQueryableAsync<Restaurant>();
                var restaurant = restaurantQuery
                    .Include(r => r.Location)
                    .ThenInclude(l => l.WardOrCommune)
                    .ThenInclude(w => w.District)
                    .ThenInclude(d => d.ProvinceOrCity)
                    .Include(r => r.RestaurantCuisineTypes)
                    .ThenInclude(r => r.CuisineType)
                    .Include(r => r.RestaurantServingTypes)
                    .ThenInclude(r => r.ServingType)
                    .Include(r => r.RestaurantAdditionalServices)
                    .ThenInclude(r => r.AdditionalService)
                    .Include(r => r.RestaurantCustomerTypes)
                    .ThenInclude(r => r.CustomerType)
                    .Include(r => r.BusinessHours)
                    .Include(r => r.Images)
                    .FirstOrDefault(r => r.Id == restaurantId);

                if (restaurant == null)
                {
                    throw new NotFoundException();
                }

                return _mapper.Map<RestaurantDetailDto>(restaurant);
            });

            return new CustomResponse<RestaurantDetailDto>
            {
                Data = data
            };
        }

        public async Task<CustomResponse<RestaurantDetailDto>> GetMyRestaurantAsync()
        {
            var data = await _cacheService.GetOrCreateAsync($"restaurant-owner-{_currentUserService.UserId}", async () =>
            {
                var restaurantQuery = await _unitOfWork.GetQueryableAsync<Restaurant>();
                var restaurant = restaurantQuery
                    .Include(r => r.Location)
                    .ThenInclude(l => l.WardOrCommune)
                    .ThenInclude(w => w.District)
                    .ThenInclude(d => d.ProvinceOrCity)
                    .Include(r => r.RestaurantCuisineTypes)
                    .ThenInclude(r => r.CuisineType)
                    .Include(r => r.RestaurantServingTypes)
                    .ThenInclude(r => r.ServingType)
                    .Include(r => r.RestaurantAdditionalServices)
                    .ThenInclude(r => r.AdditionalService)
                    .Include(r => r.RestaurantCustomerTypes)
                    .ThenInclude(r => r.CustomerType)
                    .Include(r => r.BusinessHours)
                    .Include(r => r.Images)
                    .FirstOrDefault(r => r.OwnerId == _currentUserService.UserId);
                if (restaurant == null)
                {
                    throw new NotFoundException();
                }

                return _mapper.Map<RestaurantDetailDto>(restaurant);
            });

            return new CustomResponse<RestaurantDetailDto>
            {
                Data = data
            };
        }


        public async Task<CustomResponse<List<RestaurantDto>>> GetRestaurantsAsync(FilterRestaurantDto? filter, RestaurantSortType? sortType, int pageSize, int pageNumber)
        {
            //var cacheKey = $"restaurants-{filterRestaurantDto?.ProvinceOrCityId}-{filterRestaurantDto?.DistrictId}-{filterRestaurantDto?.PriceRangePerPerson}-{filterRestaurantDto?.WardOrCommuneId}-{filterRestaurantDto?.CuisineType}-{filterRestaurantDto?.ServingType}-{filterRestaurantDto?.Sort}-{searchValue}-{pageSize}-{pageNumber}";
            var cacheKey = "restaurants";
            var restaurants = await _cacheService.GetOrCreateAsync(cacheKey, async () =>
            {
                var restaurantQuery = await _unitOfWork.GetQueryableAsync<Restaurant>();

                return await restaurantQuery
                .Include(r => r.Location)
                .ThenInclude(l => l.WardOrCommune)
                .ThenInclude(w => w.District)
                .ThenInclude(d => d.ProvinceOrCity)
                .Include(r => r.RestaurantCuisineTypes)
                .ThenInclude(r => r.CuisineType)
                .Include(r => r.RestaurantServingTypes)
                .ThenInclude(r => r.ServingType)
                .Include(r => r.RestaurantCustomerTypes)
                .ThenInclude(r => r.CustomerType)
                .Include(r => r.Images)
                .ToListAsync();
            });
            
            var restaurantsQuery = restaurants.AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchValue))
            {
                restaurantsQuery = restaurantsQuery.Where(r => r.Name.Contains(filter.SearchValue));
            }

            if (filter != null)
            {
                if (filter.ProvinceOrCityId.HasValue)
                {
                    restaurantsQuery = restaurantsQuery.Where(r => r.Location.WardOrCommune.District.ProvinceOrCityId == filter.ProvinceOrCityId);
                }
                if (filter.DistrictId.HasValue)
                {
                    restaurantsQuery = restaurantsQuery.Where(r => r.Location.WardOrCommune.DistrictId == filter.DistrictId);
                }
                if (filter.WardOrCommuneId.HasValue)
                {
                    restaurantsQuery = restaurantsQuery.Where(r => r.Location.WardOrCommuneId == filter.WardOrCommuneId);
                }
                if (filter.PriceRangePerPerson.HasValue)
                {
                    restaurantsQuery = restaurantsQuery.Where(r => r.PriceRangePerPerson == filter.PriceRangePerPerson);
                }
                if (filter.CuisineType.HasValue)
                {
                    restaurantsQuery = restaurantsQuery.Where(r => r.RestaurantCuisineTypes.Any(rc => rc.CuisineTypeId == filter.CuisineType));
                }
                if (filter.ServingType.HasValue)
                {
                    restaurantsQuery = restaurantsQuery.Where(r => r.RestaurantServingTypes.Any(rs => rs.ServingTypeId == filter.ServingType));
                }
                if (filter.CustomerType.HasValue)
                {
                    restaurantsQuery = restaurantsQuery.Where(r => r.RestaurantCustomerTypes.Any(rc => rc.CustomerTypeId == filter.CustomerType));
                }

                switch (sortType)
                {
                    case RestaurantSortType.Popular:
                        restaurantsQuery = restaurantsQuery.OrderByDescending(r => r.ReservationCount);
                        break;
                    case RestaurantSortType.PriceAscending:
                        restaurantsQuery = restaurantsQuery.OrderBy(r => r.PriceRangePerPerson);
                        break;
                    case RestaurantSortType.PriceDescending:
                        restaurantsQuery = restaurantsQuery.OrderByDescending(r => r.PriceRangePerPerson);
                        break;
                    case RestaurantSortType.Rating:
                        restaurantsQuery = restaurantsQuery.OrderByDescending(r => r.Rating);
                        break;
                    default:
                        restaurantsQuery = restaurantsQuery.OrderByDescending(r => r.ReservationCount);
                        break;
                }
            }
            int totalItems = restaurantsQuery.Count();
            var pagedRestaurant = restaurantsQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => _mapper.Map<RestaurantDto>(r))
                .ToList();

            return new CustomResponse<List<RestaurantDto>>
            {
                Data = pagedRestaurant,
                Meta = new MetaData
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
                }
            };
        }

        public async Task<CustomResponse<RestaurantDetailDto>> UpdateAsync(RestaurantUpdateDto restaurantDto)
        {
            var restaurantQuery = await _unitOfWork.GetQueryableAsync<Restaurant>();
            var restaurant = await restaurantQuery
                .Include(r => r.Images)
                .FirstOrDefaultAsync(r => r.OwnerId == _currentUserService.UserId);
            if (restaurant == null)
            {
                throw new NotFoundException();
            }

            if (restaurantDto.Name != null)
            {
                restaurant.Name = restaurantDto.Name;
            }
            if (restaurantDto.Phone != null)
            {
                restaurant.Phone = restaurantDto.Phone;
            }
            if (restaurantDto.PriceRangePerPerson.HasValue)
            {
                restaurant.PriceRangePerPerson = (PriceRangePerPerson)restaurantDto.PriceRangePerPerson;
            }
            if (restaurantDto.Capacity.HasValue)
            {
                restaurant.Capacity = restaurantDto.Capacity.Value;
            }
            if (restaurantDto.SpecialDishes != null)
            {
                restaurant.SpecialDishes = restaurantDto.SpecialDishes;
            }
            if (restaurantDto.Description != null)
            {
                restaurant.Description = restaurantDto.Description;
            }
            if (restaurantDto.Note != null)
            {
                restaurant.Note = restaurantDto.Note;
            }

            if (restaurantDto.CuisineTypes != null)
            {
                var cuisineTypes = new List<RestaurantCuisineType>();
                foreach (var cuisineType in restaurantDto.CuisineTypes)
                {
                    cuisineTypes.Add(new RestaurantCuisineType()
                    {
                        CuisineTypeId = cuisineType
                    });
                }
                restaurant.RestaurantCuisineTypes = cuisineTypes;
            }

            if (restaurantDto.ServingTypes != null)
            {
                var serviceTypes = new List<RestaurantServingType>();
                foreach (var serviceType in restaurantDto.ServingTypes)
                {
                    serviceTypes.Add(new RestaurantServingType()
                    {
                        ServingTypeId = serviceType
                    });
                }
                restaurant.RestaurantServingTypes = serviceTypes;
            }

            if (restaurantDto.CustomerTypes != null)
            {
                var customerTypes = new List<RestaurantCustomerType>();
                foreach (var customerType in restaurantDto.CustomerTypes)
                {
                    customerTypes.Add(new RestaurantCustomerType()
                    {
                        CustomerTypeId = customerType
                    });
                }
                restaurant.RestaurantCustomerTypes = customerTypes;
            }

            if (restaurantDto.Address != null)
            {
                restaurant.Location.Address = restaurantDto.Address;
            }

            if (restaurantDto.Ward.HasValue)
            {
                restaurant.Location.WardOrCommuneId = restaurantDto.Ward.Value;
            }

            if (restaurantDto.AdditionalServices != null)
            {
                var additionalServices = new List<RestaurantAdditionalService>();
                foreach (var additionalService in restaurantDto.AdditionalServices)
                {
                    additionalServices.Add(new RestaurantAdditionalService()
                    {
                        AdditionalServiceId = additionalService
                    });
                }
                restaurant.RestaurantAdditionalServices = additionalServices;
            }

            if (restaurantDto.BusinessHours != null)
            {
                var businessHours = new List<BusinessHour>();
                foreach (var businessHour in restaurantDto.BusinessHours)
                {
                    businessHours.Add(new BusinessHour()
                    {
                        DayOfWeek = (DayOfWeek)businessHour.Date,
                        OpenTime = TimeSpan.Parse(businessHour.OpenTime),
                        CloseTime = TimeSpan.Parse(businessHour.CloseTime)
                    });
                }
                restaurant.BusinessHours = businessHours;
            }

            await _unitOfWork.UpdateAsync(restaurant);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveAsync("restaurants");
            await _cacheService.RemoveAsync($"restaurant-{restaurant.Id}");
            await _cacheService.RemoveAsync($"restaurant-owner-{restaurant.OwnerId}");

            return new CustomResponse<RestaurantDetailDto>
            {
                Data = _mapper.Map<RestaurantDetailDto>(restaurant)
            };
        }

        public async Task<CustomResponse<RestaurantDetailDto>> AddImagesAsync(RestaurantAddImagesDto restaurantDto)
        {
            var restaurantQuery = await _unitOfWork.GetQueryableAsync<Restaurant>();
            var restaurant = await restaurantQuery
                .Include(r => r.Images)
                .FirstOrDefaultAsync(r => r.OwnerId == _currentUserService.UserId);

            if (restaurant == null)
            {
                throw new NotFoundException();
            }

            var restaurantImages = new List<RestaurantImage>();
            if (restaurantDto.RestaurantImages is not null)
            {
                foreach (var image in restaurantDto.RestaurantImages)
                {
                    restaurantImages.Add(new RestaurantImage()
                    {
                        URL = await _fileService.UploadFileGetUrlAsync(image),
                        ImageType = ImageType.Restaurant,
                    });
                }
            }

            var menuImages = new List<RestaurantImage>();
            if (restaurantDto.MenuImages is not null)
            {
                foreach (var image in restaurantDto.MenuImages)
                {
                    menuImages.Add(new RestaurantImage()
                    {
                        URL = await _fileService.UploadFileGetUrlAsync(image),
                        ImageType = ImageType.Menu,
                    });
                }
            }

            restaurant.Images.AddRange(restaurantImages);
            restaurant.Images.AddRange(menuImages);

            await _unitOfWork.UpdateAsync(restaurant);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveAsync("restaurants");
            await _cacheService.RemoveAsync($"restaurant-{restaurant.Id}");
            await _cacheService.RemoveAsync($"restaurant-owner-{restaurant.OwnerId}");

            return new CustomResponse<RestaurantDetailDto>
            {
                Data = _mapper.Map<RestaurantDetailDto>(restaurant)
            };
        }

        public async Task<CustomResponse<RestaurantDetailDto>> DeleteImagesAsync(List<Guid> imageIds)
        {
            var restaurantQuery = await _unitOfWork.GetQueryableAsync<Restaurant>();
            var restaurant = await restaurantQuery
                .Include(r => r.Images)
                .FirstOrDefaultAsync(r => r.OwnerId == _currentUserService.UserId);

            if (restaurant == null)
            {
                throw new NotFoundException();
            }

            var imageNamesToBeDeleted = restaurant.Images.Where(i => imageIds.Contains(i.Id)).Select(i => i.URL.Split('/').Last()).ToList();
            await _fileService.DeleteFileAsync(imageNamesToBeDeleted);

            restaurant.Images.RemoveAll(i => imageIds.Contains(i.Id));

            await _unitOfWork.UpdateAsync(restaurant);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveAsync("restaurants");
            await _cacheService.RemoveAsync($"restaurant-{restaurant.Id}");
            await _cacheService.RemoveAsync($"restaurant-owner-{restaurant.OwnerId}");

            return new CustomResponse<RestaurantDetailDto>
            {
                Data = _mapper.Map<RestaurantDetailDto>(restaurant)
            };
        }
    }
}