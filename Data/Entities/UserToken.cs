using f00die_finder_be.Common;

namespace f00die_finder_be.Data.Entities
{
    public class UserToken : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string? RefreshToken { get; set; }
        public DateTimeOffset? RefreshTokenExpiryTime { get; set; }
        public string? OTP { get; set; }
        public OTPType? OTPType { get; set; }
        public DateTimeOffset? OTPExpiryTime { get; set; }
    }
}
