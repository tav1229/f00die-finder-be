using AutoMapper;
using f00die_finder_be.Common;
using f00die_finder_be.Common.CurrentUserService;
using f00die_finder_be.Data;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Restaurant;
using f00die_finder_be.Models;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.RestaurantService
{
    public class RestaurantService : IRestaurantService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public RestaurantService(DataContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> AddAsync(RestaurantAddDto restaurantDto)
        {
            var restaurantImages = new List<RestaurantImage>();
            if (restaurantDto.RestaurantImages != null)
            {
                foreach (var image in restaurantDto.RestaurantImages)
                {
                    restaurantImages.Add(new RestaurantImage()
                    {
                        URL = image,
                        ImageType = ImageType.Restaurant,
                    });
                }
            }

            var menuImages = new List<RestaurantImage>();
            if (restaurantDto.MenuImages != null)
            {
                foreach (var image in restaurantDto.MenuImages)
                {
                    menuImages.Add(new RestaurantImage()
                    {
                        URL = image,
                        ImageType = ImageType.Menu,
                    });
                }
            }

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
            restaurantImages.AddRange(menuImages);
            var restaurant = new Restaurant()
            {
                Name = restaurantDto.Name,
                Phone = restaurantDto.Phone,
                PriceRange = restaurantDto.PriceRangePerPerson,
                Capacity = restaurantDto.Capacity,
                SpecialDishes = restaurantDto.SpecialDishes,
                Description = restaurantDto.Description,
                Note = restaurantDto.Note,
                OwnerId = _currentUserService.UserId,
                Images = restaurantImages,
                RestaurantCuisineTypes = cuisineTypes,
                RestaurantServingTypes = serviceTypes,
                Location = new Location()
                {
                    Address = restaurantDto.Address,
                    WardId = restaurantDto.Ward
                },
                RestaurantAdditionalServices = additionalServices,
                BusinessHours = businessHours,
                Status = RestaurantStatus.Pending
            };
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task DeleteAsync(Guid restaurantId)
        {
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == restaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException();
            }

            restaurant.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<RestaurantDetailDto> GetRestaurantByIdAsync(Guid restaurantId)
        {
            var restaurant = _context.Restaurants
                .Include(r => r.Location)
                .ThenInclude(l => l.WardOrCommune)
                .ThenInclude(w => w.District)
                .ThenInclude(d => d.ProvinceOrCity)
                .Include(r => r.RestaurantCuisineTypes)
                .Include(r => r.RestaurantServingTypes)
                .Include(r => r.Images)
                .FirstOrDefault(r => r.Id == restaurantId && !r.IsDeleted);
            if (restaurant == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<RestaurantDetailDto>(restaurant);

        }

        public async Task<RestaurantDetailDto> GetRestaurantByOwnerId(Guid userId)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.Location)
                .ThenInclude(l => l.WardOrCommune)
                .ThenInclude(w => w.District)
                .ThenInclude(d => d.ProvinceOrCity)
                .Include(r => r.RestaurantCuisineTypes)
                .Include(r => r.RestaurantServingTypes)
                .Include(r => r.Images)
                .FirstOrDefaultAsync(r => r.OwnerId == userId && !r.IsDeleted);
            if (restaurant == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<RestaurantDetailDto>(restaurant);

        }

        public async Task<PagedResult<RestaurantDto>> GetRestaurantsAsync(FilterRestaurantDto? filterRestaurantDto, string searchValue, int pageSize, int pageNumber)
        {
            try
            {
                var restaurants = _context.Restaurants
                    .Where(r => !r.IsDeleted)
                    .Include(r => r.Location)
                    .ThenInclude(l => l.WardOrCommune)
                    .ThenInclude(w => w.District)
                    .ThenInclude(d => d.ProvinceOrCity)
                    .Include(r => r.RestaurantCuisineTypes)
                    .Include(r => r.RestaurantServingTypes)
                    .Include(r => r.Images)
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
                    if (filterRestaurantDto.PriceRangePerPerson.HasValue)
                    {
                        restaurants = restaurants.Where(r => r.PriceRange == filterRestaurantDto.PriceRangePerPerson);
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
                            restaurants = restaurants.OrderBy(r => r.PriceRange);
                            break;
                        case "price-decrease":
                            restaurants = restaurants.OrderByDescending(r => r.PriceRange);
                            break;
                        default:
                            restaurants = restaurants.OrderByDescending(r => r.ReservationCount);
                            break;
                    }
                }
                var pagedRestaurant = await restaurants.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                return new PagedResult<RestaurantDto>
                {
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling(await restaurants.CountAsync() / (double)pageSize),
                    Items = _mapper.Map<List<RestaurantDto>>(pagedRestaurant)
                };
            }
            catch
            {
                throw new InternalServerErrorException();
            }
        }

        public Task UpdateAsync(RestaurantUpdateDto restaurant)
        {
            throw new NotImplementedException();
        }
    }
}