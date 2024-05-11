using f00die_finder_be.Common;
using System.ComponentModel.DataAnnotations;

namespace f00die_finder_be.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string HashedPassword { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        public Role Role { get; set; }

        public Restaurant? Restaurant { get; set; }

        public List<Reservation>? Reservations { get; set; }

        public List<ReviewComment>? Reviews { get; set; }
    }
}
