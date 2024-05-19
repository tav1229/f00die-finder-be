namespace f00die_finder_be.Dtos.Auth
{
    public class ForgotPasswordDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string OTP { get; set; }
    }
}
