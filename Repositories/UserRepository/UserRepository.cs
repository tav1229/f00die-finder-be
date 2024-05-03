using f00die_finder_be.Common;
using f00die_finder_be.Data;
using f00die_finder_be.Models;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user.Id;
            }
            catch
            {
                throw new InternalServerErrorException();
            }
        }

        public async Task<User> GetUserByUsernameAsync(string userName)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userName && !u.IsDeleted);
                return user;
            }
            catch
            {
                throw new InternalServerErrorException();
            }
        }

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                var users = await _context.Users.Where(u => !u.IsDeleted).ToListAsync();
                return users;
            }
            catch
            {
                throw new InternalServerErrorException();
            }
        }

    }
}