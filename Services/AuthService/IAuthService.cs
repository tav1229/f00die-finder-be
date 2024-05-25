using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Auth;

namespace f00die_finder_be.Services.AuthService
{
    public interface IAuthService
    {
        Task<CustomResponse<TokenResponse>> LoginAsync(LoginDto loginDto);
        Task<CustomResponse<TokenResponse>> RegisterAsync(RegistrationDto registrationDto);
        Task<CustomResponse<object>> GetNewOtpAsync(GetOtpDto getOtpDto);
        Task<CustomResponse<object>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<CustomResponse<TokenResponse>> RefreshTokenAsync(string refreshToken);
        Task<CustomResponse<TokenResponse>> VerifyEmailAsync(string otp);
        Task<CustomResponse<object>> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    }
}
