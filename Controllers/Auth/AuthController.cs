using f00die_finder_be.Dtos.Auth;
using f00die_finder_be.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers.Auth
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
            return Ok(await _authService.Register(userRegister));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return Ok(await _authService.Login(loginDto));
        }
    }
}