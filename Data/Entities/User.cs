using f00die_finder_be.Common;
using System.ComponentModel.DataAnnotations;

namespace f00die_finder_be.Data.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        public string HashedPassword { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        public Role Role { get; set; }

        public Restaurant? Restaurant { get; set; }

        public List<Reservation>? Reservations { get; set; }

        public List<ReviewComment>? Reviews { get; set; }
        public List<UserToken> UserTokens { get; set; }
        public UserStatus Status { get; set; }
    }
}
