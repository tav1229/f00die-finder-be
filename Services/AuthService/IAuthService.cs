using f00die_finder_be.Dtos.Auth;

namespace f00die_finder_be.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> Login(LoginDto loginDto);

        Task<string> Register(RegistrationDto registrationDto);

    }
}
