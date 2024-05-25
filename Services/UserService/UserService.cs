using f00die_finder_be.Common;
using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.User;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.UserService
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task InternalAddAsync(User user)
        {
            await _unitOfWork.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveAsync("users");
        }

        public async Task<List<User>> InternalGetUsersAsync()
        {
            return await _cacheService.GetOrCreateAsync("users", async () =>
            {
                var userQuery = await _unitOfWork.GetQueryableAsync<User>();
                var users = await userQuery.ToListAsync();
                return users;
            });
        }

        public async Task<User> InternalGetUserByEmailAsync(string email)
        {
            return await _cacheService.GetOrCreateAsync($"user-{email}", async () =>
            {
                var userQuery = await _unitOfWork.GetQueryableAsync<User>();
                var user = await userQuery.FirstOrDefaultAsync(u => u.Email == email);
                return user;
            });
        }

        public async Task<User> InternalGetUserByIdAsync(Guid id)
        {
            return await _cacheService.GetOrCreateAsync($"user-{id}", async () =>
            {
                var userQuery = await _unitOfWork.GetQueryableAsync<User>();
                var user = await userQuery.FirstOrDefaultAsync(u => u.Id == id);
                return user;
            });
        }

        public async Task InternalDeleteAsync(User user, bool isHardDelete)
        {
            await _unitOfWork.DeleteAsync(user, isHardDelete);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveAsync($"user-{user.Email}");
            await _cacheService.RemoveAsync($"user-{user.Id}");
            await _cacheService.RemoveAsync("users");
        }

        public async Task<CustomResponse<UserDetailDto>> GetMyInfoAsync()
        {
            var data = await _cacheService.GetOrCreateAsync($"user-{_currentUserService.UserId}", async () =>
            {
                var userQuery = await _unitOfWork.GetQueryableAsync<User>();
                var user = await userQuery.FirstOrDefaultAsync(u => u.Id == _currentUserService.UserId);
                return _mapper.Map<UserDetailDto>(user);
            });

            return new CustomResponse<UserDetailDto>
            {
                Data = data
            };
        }

        public async Task<CustomResponse<UserDetailDto>> GetUserByIdAsync(Guid id)
        {
            var data = await _cacheService.GetOrCreateAsync($"user-{id}", async () =>
            {
                var userQuery = await _unitOfWork.GetQueryableAsync<User>();
                var user = await userQuery.FirstOrDefaultAsync(u => u.Id == id);
                return _mapper.Map<UserDetailDto>(user);
            });

            return new CustomResponse<UserDetailDto>
            {
                Data = data
            };
        }

        public async Task<CustomResponse<UserDetailDto>> UpdateAsync(UserUpdateDto dto)
        {
            var userQuery = await _unitOfWork.GetQueryableAsync<User>();
            var user = await userQuery.FirstOrDefaultAsync(u => u.Id == _currentUserService.UserId);
            if (user == null)
            {
                throw new NotFoundException();
            }

            if (!string.IsNullOrEmpty(dto.FullName))
            {
                user.FullName = dto.FullName;
            }

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                user.PhoneNumber = dto.PhoneNumber;
            }

            if (!string.IsNullOrEmpty(dto.Email))
            {
                user.Email = dto.Email;
            }

            await _unitOfWork.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveAsync($"user-{_currentUserService.UserId}");
            await _cacheService.RemoveAsync($"user-{user.Email}");
            await _cacheService.RemoveAsync("users");

            return new CustomResponse<UserDetailDto>
            {
                Data = _mapper.Map<UserDetailDto>(user)
            };
        }
    }
}