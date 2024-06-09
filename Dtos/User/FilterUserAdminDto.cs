using f00die_finder_be.Common;

namespace f00die_finder_be.Dtos.User
{
    public class FilterUserAdminDto
    {
        public Role? Role { get; set; }
        public UserStatus? Status { get; set; }
    }
}