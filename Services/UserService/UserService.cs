using f00die_finder_be.Entities;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.UserService
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<Guid> AddAsync(User user)
        {
            await _unitOfWork.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveAsync("users");
            return user.Id;
        }

        public async Task<User> GetUserByUsernameAsync(string userName)
        {
            return await _cacheService.GetOrCreateAsync($"user-{userName}", async () =>
            {
                var userQuery = await _unitOfWork.GetAllAsync<User>();
                var user = await userQuery.FirstOrDefaultAsync(u => u.Username == userName);
                return user;
            });
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _cacheService.GetOrCreateAsync("users", async () =>
            {
                var userQuery = await _unitOfWork.GetAllAsync<User>();
                var users = await userQuery.ToListAsync();
                return users;
            });
        }
    }
}