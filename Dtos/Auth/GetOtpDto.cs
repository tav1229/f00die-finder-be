using f00die_finder_be.Common;
using System.ComponentModel.DataAnnotations;

namespace f00die_finder_be.Dtos.Auth
{
    public class GetOtpDto
    {
        [Required]
        public OTPType OTPType { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
