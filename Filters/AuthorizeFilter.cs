using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using f00die_finder_be.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace f00die_finder_be.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthorizeFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly Role[]? _roles;

        public AuthorizeFilter(Role[] roles)
        {
            _roles = roles;
        }

        public AuthorizeFilter() { }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
                var token = GetValue(context, "Authorization");
                if (string.IsNullOrEmpty(token))
                {
                    throw new InvalidTokenException();
                }

                if (token.ToString().StartsWith("Bearer "))
                {
                    token = token.ToString().Substring(7);
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuration["SecretKey"]);
                tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    },
                    out SecurityToken validatedToken
                );

                var jwtToken =
                    (JwtSecurityToken)validatedToken
                    ?? throw new InvalidTokenException();
                var claims = jwtToken.Claims;
                var userId = claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                var role = claims.FirstOrDefault(x => x.Type == "Role")?.Value;

                if (_roles is not null && !_roles.Contains((Role)Enum.Parse(typeof(Role), role)))
                {
                    throw new Common.UnauthorizedAccessException();
                }

                if (!context.HttpContext.Request.Path.Value.Contains("verify-email"))
                {
                    var isVerified = bool.Parse(claims.FirstOrDefault(x => x.Type == "IsVerified")?.Value);

                    if (isVerified == false)
                        throw new UnverifiedEmailException();
                }

                var identity = new ClaimsIdentity(context.HttpContext.User.Identity);
                identity.AddClaim(new Claim("UserId", userId));
                identity.AddClaim(new Claim("Role", role));

                var principal = new ClaimsPrincipal(identity);
                context.HttpContext.User = principal;
                await Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static string GetValue(AuthorizationFilterContext context, string key)
        {
            try
            {
                var apiKeyHeader = context.HttpContext.Request.Headers[key].ToString();
                if (string.IsNullOrEmpty(apiKeyHeader))
                    apiKeyHeader = context.HttpContext.Request.Cookies[key].ToString();
                return apiKeyHeader;
            }
            catch
            {
                throw new InvalidTokenException();
            }
        }
    }
}
