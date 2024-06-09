using f00die_finder_be.Common;

namespace f00die_finder_be.Dtos.User
{
    public class UserAdminDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public Role Role { get; set; }

        public UserStatus Status { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
    }
}
