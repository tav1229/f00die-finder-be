using f00die_finder_be.Dtos.Auth;
using f00die_finder_be.Filters;
using f00die_finder_be.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationDto userRegister)
        {
            return Ok(await _authService.RegisterAsync(userRegister));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return Ok(await _authService.LoginAsync(loginDto));
        }

        [HttpPost("get-otp")]
        public async Task<IActionResult> GetNewOtp(GetOtpDto getOtpDto)
        {
            var result = await _authService.GetNewOtpAsync(getOtpDto);
            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var result = await _authService.ForgotPasswordAsync(forgotPasswordDto);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string refreshTokenDto)
        {
            var result = await _authService.RefreshTokenAsync(refreshTokenDto);
            return Ok(result);
        }

        [AuthorizeFilterAttribute]
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(string otp)
        {
            var result = await _authService.VerifyEmailAsync(otp);
            return Ok(result);
        }

        [AuthorizeFilterAttribute]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var result = await _authService.ChangePasswordAsync(changePasswordDto);
            return Ok(result);
        }
    }
}