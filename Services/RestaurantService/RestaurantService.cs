using f00die_finder_be.Common;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Restaurant;
using f00die_finder_be.Entities;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.RestaurantService
{
    public class RestaurantService : BaseService, IRestaurantService
    {
        public RestaurantService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<Guid> AddAsync(RestaurantAddDto restaurantDto)
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
                Location = new f00die_finder_be.Entities.Location()
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

            return restaurant.Id;
        }

        public async Task DeleteAsync(Guid restaurantId)
        {
            var restaurantQuery = await _unitOfWork.GetAllAsync<Restaurant>();
            var restaurant = await restaurantQuery.FirstOrDefaultAsync(r => r.Id == restaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException();
            }

            await _unitOfWork.DeleteAsync(restaurant);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveByPrefixAsync("restaurant");
        }

        public async Task<RestaurantDetailDto> GetRestaurantByIdAsync(Guid restaurantId)
        {
            return await _cacheService.GetOrCreateAsync($"restaurant-{restaurantId}", async () =>
            {
                var restaurantQuery = await _unitOfWork.GetAllAsync<Restaurant>();
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
                    .Include(r => r.BusinessHours)
                    .Include(r => r.Images)
                    .FirstOrDefault(r => r.Id == restaurantId);

                if (restaurant == null)
                {
                    throw new NotFoundException();
                }

                return _mapper.Map<RestaurantDetailDto>(restaurant);
            });
        }

        public async Task<RestaurantDetailDto> GetRestaurantByOwnerIdAsync(Guid userId)
        {
            return await _cacheService.GetOrCreateAsync($"restaurant-owner-{userId}", async () =>
            {
                var restaurantQuery = await _unitOfWork.GetAllAsync<Restaurant>();
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
                    .Include(r => r.BusinessHours)
                    .Include(r => r.Images)
                    .FirstOrDefault(r => r.OwnerId == userId);
                if (restaurant == null)
                {
                    throw new NotFoundException();
                }

                return _mapper.Map<RestaurantDetailDto>(restaurant);
            });
        }


        public async Task<PagedResult<RestaurantDto>> GetRestaurantsAsync(FilterRestaurantDto? filterRestaurantDto, string searchValue, int pageSize, int pageNumber)
        {
            var cacheKey = $"restaurants-{filterRestaurantDto?.ProvinceOrCityId}-{filterRestaurantDto?.DistrictId}-{filterRestaurantDto?.PriceRangePerPerson}-{filterRestaurantDto?.WardOrCommuneId}-{filterRestaurantDto?.CuisineType}-{filterRestaurantDto?.ServingType}-{filterRestaurantDto?.Sort}-{searchValue}-{pageSize}-{pageNumber}";
            return await _cacheService.GetOrCreateAsync(cacheKey, async () =>
            {
                var restaurantQuery = await _unitOfWork.GetAllAsync<Restaurant>();

                var restaurants = restaurantQuery
                .Include(r => r.Location)
                .ThenInclude(l => l.WardOrCommune)
                .ThenInclude(w => w.District)
                .ThenInclude(d => d.ProvinceOrCity)
                .Include(r => r.RestaurantCuisineTypes)
                .ThenInclude(r => r.CuisineType)
                .Include(r => r.RestaurantServingTypes)
                .ThenInclude(r => r.ServingType)
                .Include(r => r.Images)
                .Where(r => string.IsNullOrEmpty(searchValue) || r.Name.Contains(searchValue))
                .AsQueryable();

                if (filterRestaurantDto != null)
                {
                    if (filterRestaurantDto.ProvinceOrCityId.HasValue)
                    {
                        restaurants = restaurants.Where(r => r.Location.WardOrCommune.District.ProvinceOrCityId == filterRestaurantDto.ProvinceOrCityId);
                    }
                    if (filterRestaurantDto.DistrictId.HasValue)
                    {
                        restaurants = restaurants.Where(r => r.Location.WardOrCommune.DistrictId == filterRestaurantDto.DistrictId);
                    }
                    if (filterRestaurantDto.WardOrCommuneId.HasValue)
                    {
                        restaurants = restaurants.Where(r => r.Location.WardOrCommuneId == filterRestaurantDto.WardOrCommuneId);
                    }
                    if (filterRestaurantDto.PriceRangePerPerson.HasValue)
                    {
                        restaurants = restaurants.Where(r => r.PriceRangePerPerson == filterRestaurantDto.PriceRangePerPerson);
                    }
                    if (filterRestaurantDto.CuisineType.HasValue)
                    {
                        restaurants = restaurants.Where(r => r.RestaurantCuisineTypes.Any(rc => rc.CuisineTypeId == filterRestaurantDto.CuisineType));
                    }
                    if (filterRestaurantDto.ServingType.HasValue)
                    {
                        restaurants = restaurants.Where(r => r.RestaurantServingTypes.Any(rs => rs.ServingTypeId == filterRestaurantDto.ServingType));
                    }

                    switch (filterRestaurantDto.Sort)
                    {
                        case "popular":
                            restaurants = restaurants.OrderByDescending(r => r.ReservationCount);
                            break;
                        case "price-increase":
                            restaurants = restaurants.OrderBy(r => r.PriceRangePerPerson);
                            break;
                        case "price-decrease":
                            restaurants = restaurants.OrderByDescending(r => r.PriceRangePerPerson);
                            break;
                        default:
                            restaurants = restaurants.OrderByDescending(r => r.ReservationCount);
                            break;
                    }
                }
                var pagedRestaurant = await restaurants.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                var pagedResult = new PagedResult<RestaurantDto>
                {
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling(await restaurants.CountAsync() / (double)pageSize),
                    Items = _mapper.Map<List<RestaurantDto>>(pagedRestaurant)
                };

                return pagedResult;
            });
        }

        public async Task UpdateAsync(RestaurantUpdateDto restaurantDto)
        {
            var restaurantQuery = await _unitOfWork.GetAllAsync<Restaurant>();
            var restaurant = await restaurantQuery
                .Include(r => r.Images)
                .FirstOrDefaultAsync(r => r.Id == restaurantDto.Id);
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

            await _cacheService.RemoveByPrefixAsync("restaurant");
        }

        public async Task UpdateImagesAsync(RestaurantUpdateImagesDto restaurantDto)
        {
            var restaurantQuery = await _unitOfWork.GetAllAsync<Restaurant>();
            var restaurant = await restaurantQuery
                .Include(r => r.Images)
                .FirstOrDefaultAsync(r => r.Id == restaurantDto.Id);

            if (restaurant == null)
            {
                throw new NotFoundException();
            }

            var restaurantImages = new List<RestaurantImage>();
            foreach (var image in restaurantDto.RestaurantImages)
            {

                var restaurantImage = new RestaurantImage()
                {
                    URL = await _fileService.UploadImageAsync(image),
                    ImageType = ImageType.Restaurant,
                };

                restaurantImages.Add(restaurantImage);
            }
            foreach (var image in restaurant.Images.Where(i => i.ImageType == ImageType.Restaurant))
            {
                await _unitOfWork.DeleteAsync(image, isHardDeleted: true);
            }
            restaurant.Images.AddRange(restaurantImages);

            var menuImages = new List<RestaurantImage>();
            foreach (var image in restaurantDto.MenuImages)
            {
                menuImages.Add(new RestaurantImage()
                {
                    URL = await _fileService.UploadImageAsync(image),
                    ImageType = ImageType.Menu,
                });
            }
            foreach (var image in restaurant.Images.Where(i => i.ImageType == ImageType.Menu))
            {
                await _unitOfWork.DeleteAsync(image, isHardDeleted: true);
            }
            restaurant.Images.AddRange(menuImages);

            await _unitOfWork.UpdateAsync(restaurant);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveByPrefixAsync("restaurant");
        }
    }
}