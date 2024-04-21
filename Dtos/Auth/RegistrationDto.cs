using System.ComponentModel.DataAnnotations;

namespace f00die_finder_be.Dtos.Auth
{
    public class RegistrationDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public short Role { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}