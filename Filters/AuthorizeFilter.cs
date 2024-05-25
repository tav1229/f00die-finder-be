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
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
                var token = GetValue(context, "Authorization");
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Token is required");
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
                    ?? throw new Exception("Invalid token");
                var claims = jwtToken.Claims;
                var userId = claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

                if (!context.HttpContext.Request.Path.Value.Contains("verify-email"))
                {
                    var isVerified = bool.Parse(claims.FirstOrDefault(x => x.Type == "IsVerified")?.Value);

                    if (isVerified == false)
                        throw new UnverifiedEmailException();
                }

                var identity = new ClaimsIdentity(context.HttpContext.User.Identity);
                identity.AddClaim(new Claim("UserId", userId));

                var principal = new ClaimsPrincipal(identity);
                context.HttpContext.User = principal;
                await Task.CompletedTask;
            }
            catch (Exception e)
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
            catch (Exception)
            {
                return null;
            }
        }
    }
}
