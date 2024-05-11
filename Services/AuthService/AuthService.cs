using f00die_finder_be.Common;
using f00die_finder_be.Dtos.Auth;
using f00die_finder_be.Entities;
using f00die_finder_be.Services.UserService;

namespace f00die_finder_be.Services.AuthService
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _userService = userService;

        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var currentUser = await _userService.GetUserByUsernameAsync(loginDto.Username);
            if (currentUser == null)
            {
                throw new InvalidCredentialsException();
            }

            var hashedPassword = SecurityFunction.HashPassword(loginDto.Password, currentUser.PasswordSalt);
            if (currentUser.HashedPassword != hashedPassword || currentUser.Role != loginDto.Role)
            {
                throw new InvalidCredentialsException();
            }

            return SecurityFunction.GenerateToken(new ClaimData()
            {
                UserId = currentUser.Id,
                Username = currentUser.Username,
                Role = currentUser.Role,
                FullName = currentUser.FullName,
                Phone = currentUser.Phone,
                Email = currentUser.Email
            }, _configuration);
        }

        public async Task<string> Register(RegistrationDto registrationDto)
        {
            var users = await _userService.GetUsersAsync();
            if (users.Any(u => u.Username == registrationDto.Username))
            {
                throw new UsernameIsAlreadyExistedException();
            }
            else if (users.Any(u => u.Email == registrationDto.Email))
            {
                throw new EmailIsAlreadyExistedException();
            }
            else if (users.Any(u => u.Phone == registrationDto.Phone))
            {
                throw new PhoneIsAlreadyExistedException();
            }

            var passwordSalt = SecurityFunction.GenerateRandomString();
            var hashedPassword = SecurityFunction.HashPassword(registrationDto.Password, passwordSalt);

            var newUser = new User
            {
                Username = registrationDto.Username,
                FullName = registrationDto.FullName,
                Phone = registrationDto.Phone,
                PasswordSalt = passwordSalt,
                HashedPassword = hashedPassword,
                Email = registrationDto.Email,
                Role = (Role)registrationDto.Role
            };

            await _userService.AddAsync(newUser);

            return SecurityFunction.GenerateToken(new ClaimData()
            {
                UserId = newUser.Id,
                Username = newUser.Username,
                Role = newUser.Role,
                FullName = newUser.FullName,
                Phone = newUser.Phone,
                Email = newUser.Email
            }, _configuration);
        }
    }
}