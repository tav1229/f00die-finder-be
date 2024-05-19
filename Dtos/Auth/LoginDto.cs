using f00die_finder_be.Common;
using System.ComponentModel.DataAnnotations;

namespace f00die_finder_be.Dtos.Auth
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}