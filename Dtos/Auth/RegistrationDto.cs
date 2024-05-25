using System.ComponentModel.DataAnnotations;

namespace f00die_finder_be.Dtos.Auth
{
    public class RegistrationDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public short Role { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}