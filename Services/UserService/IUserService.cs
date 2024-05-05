using f00die_finder_be.Models;

namespace f00die_finder_be.Services.UserService
{
    public interface IUserService 
    {
        Task<User> GetUserByUsernameAsync(string userName);
        Task<List<User>> GetUsersAsync();
        Task<Guid> AddAsync(User user);
    }
}