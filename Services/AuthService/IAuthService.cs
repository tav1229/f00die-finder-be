using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Auth;

namespace f00die_finder_be.Services.AuthService
{
    public interface IAuthService
    {
        Task<CustomResponse<LoginRegisterResponseDto>> LoginAsync(LoginDto loginDto);

        Task<CustomResponse<LoginRegisterResponseDto>> RegisterAsync(RegistrationDto registrationDto);
        Task<CustomResponse<object>> GetNewOtpAsync(GetOtpDto getOtpDto);
        Task<CustomResponse<object>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<CustomResponse<LoginRegisterResponseDto>> RefreshTokenAsync(string refreshToken);
    }
}
