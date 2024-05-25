using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.User;

namespace f00die_finder_be.Services.UserService
{
    public interface IUserService 
    {
        Task<User> GetUserByEmailAsync(string email); // not public to be used in the controller
        Task<List<User>> GetUsersAsync(); // not public to be used in the controller
        Task<Guid> AddAsync(User user);  // not public to be used in the controller

        Task<CustomResponse<UserDetailDto>> GetUserByIdAsync(Guid id);
        Task<CustomResponse<UserDetailDto>> GetMyInfoAsync();
        Task<CustomResponse<UserDetailDto>> UpdateAsync(UserUpdateDto user);
    }
}