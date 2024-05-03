//using f00die_finder_be.Dtos;
//using f00die_finder_be.Dtos.Restaurant;
//using f00die_finder_be.Models;

//namespace f00die_finder_be.Repositories.RestaurantRepository
//{
//    public interface IRestaurantRepository
//    {
//        Task<PagedResult<Restaurant>> GetRestaurantsAsync(FilterRestaurantDto? filterRestaurantDto, string searchValue, int pageSize, int pageNumber);
//        Task<Restaurant> GetRestaurantByIdAsync(Guid idRestaurant);
//        Task<Restaurant> GetRestaurantByOwnerId(Guid idUser);
//        Task<Guid> AddAsync(Restaurant restaurant);
//        Task UpdateAsync(Restaurant restaurant);
//        Task DeleteAsync(Restaurant restaurant);
//    }
//}