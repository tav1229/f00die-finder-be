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
            var userQuery = await _unitOfWork.GetQueryableAsync<User>();
            var users = await userQuery.ToListAsync();
            return users;
        }

        public async Task<User> InternalGetUserByEmailAsync(string email)
        {
            var userQuery = await _unitOfWork.GetQueryableAsync<User>();
            var user = await userQuery.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<User> InternalGetUserByIdAsync(Guid id)
        {
            var userQuery = await _unitOfWork.GetQueryableAsync<User>();
            var user = await userQuery.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task InternalDeleteAsync(User user, bool isHardDelete)
        {
            await _unitOfWork.DeleteAsync(user, isHardDelete);
            await _unitOfWork.SaveChangesAsync();

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

        public async Task<CustomResponse<UserDetailDto>> UpdateMyInfoAsync(UserUpdateDto dto)
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
            await _cacheService.RemoveAsync("users");


            var data = _mapper.Map<UserDetailDto>(user);

            await _cacheService.SetAsync($"user-{_currentUserService.UserId}", data);

            return new CustomResponse<UserDetailDto>
            {
                Data = data
            };
        }

        public async Task<CustomResponse<List<UserAdminDto>>> GetUsersGetRestaurantsAdminAsync(FilterUserAdminDto? filter, int pageSize, int pageNumber)
        {
            var userQuery = await _unitOfWork.GetQueryableAsync<User>();
            
            if (filter is not null)
            {
                if (filter.Role is not null)
                {
                    userQuery = userQuery.Where(u => u.Role == filter.Role);
                }

                if (filter.Status is not null)
                {
                    userQuery = userQuery.Where(u => u.Status == filter.Status);
                }

                if (filter.SearchValue is not null)
                {
                    userQuery = userQuery.Where(u => u.FullName.Contains(filter.SearchValue) || u.Email.Contains(filter.SearchValue) || u.PhoneNumber.Contains(filter.SearchValue));
                }
            }

            var users = await userQuery
                .OrderByDescending(u => u.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var data = _mapper.Map<List<UserAdminDto>>(users);

            return new CustomResponse<List<UserAdminDto>>
            {
                Data = data,
                Meta = new MetaData
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(await userQuery.CountAsync() / (double)pageSize),
                    TotalCount = await userQuery.CountAsync()
                }
            };
        }

        public async Task<CustomResponse<object>> ChangeUserStatusAdminAsync(Guid userId, UserStatus status)
        {
            var userQuery = await _unitOfWork.GetQueryableAsync<User>();
            var user = await userQuery.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new NotFoundException();
            }

            user.Status = status;
            await _unitOfWork.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveAsync($"user-{userId}");
            await _cacheService.RemoveAsync("users");
            
            return new CustomResponse<object>();
        }
    }
}