using f00die_finder_be.Common;
using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Auth;
using f00die_finder_be.Services.UserService;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.AuthService
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IUserService _userService;
        private int _tokenTimeOut => int.Parse(_configuration["TokenTimeOut"]);
        private int _refreshTokenTimeOut => int.Parse(_configuration["RefreshTokenTimeOut"]);
        private int _otpTimeOut => int.Parse(_configuration["OtpTimeOut"]);

        public AuthService(IUserService userService, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _userService = userService;

        }

        public async Task<CustomResponse<LoginRegisterResponseDto>> LoginAsync(LoginDto loginDto)
        {
            var currentUser = await _userService.GetUserByUsernameAsync(loginDto.Username);
            if (currentUser == null)
            {
                throw new InvalidCredentialsException();
            }

            var hashedPassword = SecurityFunction.HashPassword(loginDto.Password, currentUser.PasswordSalt);
            if (currentUser.HashedPassword != hashedPassword)
            {
                throw new InvalidCredentialsException();
            }

            var refreshToken = SecurityFunction.GenerateRandomString();
            var userToken = new UserToken
            {
                UserId = currentUser.Id,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTimeOffset.Now.AddMinutes(_refreshTokenTimeOut)
            };

            await _unitOfWork.AddAsync(userToken);
            await _unitOfWork.SaveChangesAsync();


            return new CustomResponse<LoginRegisterResponseDto>
            {
                Data = new LoginRegisterResponseDto
                {
                    UserId = currentUser.Id,
                    AccessToken = SecurityFunction.GenerateToken(new ClaimData()
                    {
                        UserId = currentUser.Id,
                    }, _configuration),
                    AccessTokenExpiryTime = DateTimeOffset.Now.AddMinutes(_tokenTimeOut),
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = userToken.RefreshTokenExpiryTime.Value,
                    Role = currentUser.Role
                }
            };
        }

        public async Task<CustomResponse<LoginRegisterResponseDto>> RegisterAsync(RegistrationDto registrationDto)
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
            else if (users.Any(u => u.PhoneNumber == registrationDto.PhoneNumber))
            {
                throw new PhoneIsAlreadyExistedException();
            }

            var passwordSalt = SecurityFunction.GenerateRandomString();
            var hashedPassword = SecurityFunction.HashPassword(registrationDto.Password, passwordSalt);

            var newUser = new User
            {
                Username = registrationDto.Username,
                FullName = registrationDto.FullName,
                PhoneNumber = registrationDto.PhoneNumber,
                PasswordSalt = passwordSalt,
                HashedPassword = hashedPassword,
                Email = registrationDto.Email,
                Role = (Role)registrationDto.Role
            };

            await _userService.AddAsync(newUser);

            var refreshToken = SecurityFunction.GenerateRandomString();
            var userToken = new UserToken
            {
                UserId = newUser.Id,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTimeOffset.Now.AddMinutes(_refreshTokenTimeOut)
            };

            await _unitOfWork.AddAsync(userToken);
            await _unitOfWork.SaveChangesAsync();

            return new CustomResponse<LoginRegisterResponseDto>
            {
                Data = new LoginRegisterResponseDto
                {
                    UserId = newUser.Id,
                    AccessToken = SecurityFunction.GenerateToken(new ClaimData()
                    {
                        UserId = newUser.Id,
                    }, _configuration),
                    AccessTokenExpiryTime = DateTimeOffset.Now.AddMinutes(_tokenTimeOut),
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = userToken.RefreshTokenExpiryTime.Value,
                    Role = newUser.Role
                }
            };
        }

        public async Task<CustomResponse<object>> GetNewOtpAsync(GetOtpDto getOtpDto)
        {
            var user = await _userService.GetUserByEmailAsync(getOtpDto.Email);
            if (user == null)
            {
                throw new NotFoundException();
            }

            var OTP = SecurityFunction.GenerateOTP();
            var existingOtp = await (await _unitOfWork.GetQueryableAsync<UserToken>())
                .FirstOrDefaultAsync(u => u.UserId == user.Id && u.OTPType == getOtpDto.OTPType);
            if (existingOtp != null)
            {
                existingOtp.OTP = OTP;
                existingOtp.OTPExpiryTime = DateTimeOffset.Now.AddMinutes(_otpTimeOut);
                await _unitOfWork.UpdateAsync(existingOtp);
            }
            else
            {
                var userToken = new UserToken
                {
                    UserId = user.Id,
                    OTP = OTP,
                    OTPType = getOtpDto.OTPType,
                    OTPExpiryTime = DateTimeOffset.Now.AddMinutes(_otpTimeOut)
                };
                await _unitOfWork.AddAsync(userToken);

            }
            await _unitOfWork.SaveChangesAsync();
            await _mailService.SendEmailAsync(user.Email,
                                              MailConsts.ForgotPassword.Subject,
                                              MailConsts.ForgotPassword.Template,
                                              new { OTP = OTP });
            return new CustomResponse<object>();
        }

        public async Task<CustomResponse<object>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userService.GetUserByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
            {
                throw new NotFoundException();
            }

            var userToken = await (await _unitOfWork.GetQueryableAsync<UserToken>())
                .FirstOrDefaultAsync(u => u.UserId == user.Id && u.OTPType == OTPType.ForgotPassword && u.OTP == forgotPasswordDto.OTP);

            if (userToken == null || userToken.OTPExpiryTime < DateTimeOffset.Now)
            {
                throw new InvalidOtpException();
            }

            var passwordSalt = SecurityFunction.GenerateRandomString();
            var hashedPassword = SecurityFunction.HashPassword(forgotPasswordDto.Password, passwordSalt);

            user.PasswordSalt = passwordSalt;
            user.HashedPassword = hashedPassword;

            await _unitOfWork.UpdateAsync(user);

            userToken.OTP = null;
            userToken.OTPExpiryTime = null;
            await _unitOfWork.UpdateAsync(userToken);

            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveAsync($"user-{user.Id}");
            await _cacheService.RemoveAsync($"user-{user.Email}");
            await _cacheService.RemoveAsync($"user-{user.Username}");
            await _cacheService.RemoveAsync("users");

            return new CustomResponse<object>();
        }

        public async Task<CustomResponse<LoginRegisterResponseDto>> RefreshTokenAsync(string refreshToken)
        {
            var userToken = await (await _unitOfWork.GetQueryableAsync<UserToken>())
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiryTime > DateTimeOffset.Now);
            if (userToken == null)
            {
                throw new InvalidRefreshTokenException();
            }

            var user = (await _userService.GetUserByIdAsync(userToken.UserId)).Data;
            if (user == null)
            {
                throw new NotFoundException();
            }

            var newRefreshToken = SecurityFunction.GenerateRandomString();
            userToken.RefreshToken = newRefreshToken;
            userToken.RefreshTokenExpiryTime = DateTimeOffset.Now.AddMinutes(_refreshTokenTimeOut);

            await _unitOfWork.UpdateAsync(userToken);
            await _unitOfWork.SaveChangesAsync();

            return new CustomResponse<LoginRegisterResponseDto>
            {
                Data = new LoginRegisterResponseDto
                {
                    UserId = user.Id,
                    AccessToken = SecurityFunction.GenerateToken(new ClaimData()
                    {
                        UserId = user.Id,
                    }, _configuration),
                    AccessTokenExpiryTime = DateTimeOffset.Now.AddMinutes(_tokenTimeOut),
                    RefreshToken = newRefreshToken,
                    RefreshTokenExpiryTime = userToken.RefreshTokenExpiryTime.Value,
                    Role = user.Role
                }
            };
        }
    }
}