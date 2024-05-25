using f00die_finder_be.Common;

namespace f00die_finder_be.Dtos.Auth
{
    public class TokenResponse
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
        public DateTimeOffset AccessTokenExpiryTime { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset RefreshTokenExpiryTime { get; set; }
        public Role Role { get; set; }
    }
}