//using f00die_finder_be.Common;
//using f00die_finder_be.Data;
//using f00die_finder_be.Dtos;
//using f00die_finder_be.Dtos.Restaurant;
//using f00die_finder_be.Models;
//using Microsoft.EntityFrameworkCore;

//namespace f00die_finder_be.Repositories.RestaurantRepository
//{
//    public class RestaurantRepository : IRestaurantRepository
//    {
//        private readonly DataContext _context;

//        public RestaurantRepository(DataContext context)
//        {
//            _context = context;
//        }

//        public async Task<Guid> AddAsync(Restaurant restaurant)
//        {
//            try
//            {
//                await _context.Restaurants.AddAsync(restaurant);
//                await _context.SaveChangesAsync();
//                return restaurant.Id;
//            }
//            catch
//            {
//                throw new InternalServerErrorException();
//            }
//        }

//        public async Task DeleteAsync(Restaurant restaurant)
//        {
//            try
//            {
//                _context.Restaurants.Remove(restaurant);
//                await _context.SaveChangesAsync();
//            }
//            catch
//            {
//                throw new InternalServerErrorException();
//            }
//        }

//        public async Task<Restaurant> GetRestaurantByIdAsync(Guid idRestaurant)
//        {
//            try
//            {
//                var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == idRestaurant && !r.IsDeleted);
//                return restaurant;
//            }
//            catch
//            {
//                throw new InternalServerErrorException();
//            }
//        }

//        public async Task<Restaurant> GetRestaurantByOwnerId(Guid idUser)
//        {
//            try
//            {
//                var restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.OwnerId == idUser && !r.IsDeleted);
//                return restaurant;
//            }
//            catch
//            {
//                throw new InternalServerErrorException();
//            }
//        }

//        public async Task<PagedResult<Restaurant>> GetRestaurantsAsync(FilterRestaurantDto? filterRestaurantDto, string searchValue, int pageSize, int pageNumber)
//        {
//            try
//            {
//                var restaurants = _context.Restaurants
//                    .Where(r => !r.IsDeleted)
//                    .Include(r => r.Location)
//                    .ThenInclude(l => l.WardOrCommune)
//                    .ThenInclude(w => w.District)
//                    .ThenInclude(d => d.ProvinceOrCity)
//                    .Include(r => r.RestaurantCuisineTypes)
//                    .Include(r => r.RestaurantServingTypes)
//                    .Include(r => r.Images)
//                    .AsQueryable();

//                if (filterRestaurantDto != null)
//                {
//                    if (filterRestaurantDto.ProvinceOrCityId.HasValue)
//                    {
//                        restaurants = restaurants.Where(r => r.Location.WardOrCommune.District.ProvinceOrCityId == filterRestaurantDto.ProvinceOrCityId);
//                    }
//                    if (filterRestaurantDto.DistrictId.HasValue)
//                    {
//                        restaurants = restaurants.Where(r => r.Location.WardOrCommune.DistrictId == filterRestaurantDto.DistrictId);
//                    }
//                    if (filterRestaurantDto.PriceRangePerPerson.HasValue)
//                    {
//                        restaurants = restaurants.Where(r => r.PriceRange == filterRestaurantDto.PriceRangePerPerson);
//                    }
//                    if (filterRestaurantDto.CuisineType.HasValue)
//                    {
//                        restaurants = restaurants.Where(r => r.RestaurantCuisineTypes.Any(rc => rc.CuisineTypeId == filterRestaurantDto.CuisineType));
//                    }
//                    if (filterRestaurantDto.ServingType.HasValue)
//                    {
//                        restaurants = restaurants.Where(r => r.RestaurantServingTypes.Any(rs => rs.ServingTypeId == filterRestaurantDto.ServingType));
//                    }

//                    switch (filterRestaurantDto.Sort)
//                    {
//                        case "popular":
//                            restaurants = restaurants.OrderByDescending(r => r.ReservationCount);
//                            break;
//                        case "price-increase":
//                            restaurants = restaurants.OrderBy(r => r.PriceRange);
//                            break;
//                        case "price-decrease":
//                            restaurants = restaurants.OrderByDescending(r => r.PriceRange);
//                            break;
//                        default:
//                            restaurants = restaurants.OrderByDescending(r => r.ReservationCount);
//                            break;
//                    }
//                }

//                return new PagedResult<Restaurant>
//                {
//                    PageSize = pageSize,
//                    CurrentPage = pageNumber,
//                    TotalPages = (int)Math.Ceiling(await restaurants.CountAsync() / (double)pageSize),
//                    Items = await restaurants.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()
//                };
//            }
//            catch
//            {
//                throw new InternalServerErrorException();
//            }
//        }

//        public Task<PagedResult<Restaurant>> GetRestaurantsAsync(FilterRestaurantDto? filterRestaurantDto, string searchValue, int pageSize, int pageNumber)
//        {
//            throw new NotImplementedException();
//        }

//        public Task UpdateAsync(Restaurant restaurant)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}