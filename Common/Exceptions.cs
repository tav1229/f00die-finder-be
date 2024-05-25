using System.Net;

namespace f00die_finder_be.Common
{
    public class CustomException : Exception
    {
        public CustomException(string message, int statusCode = (int)HttpStatusCode.BadRequest) : base(message)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
    }

    public class InvalidCredentialsException : CustomException
    {
        public InvalidCredentialsException()
            : base($"Invalid credentials.") { }
    }

    public class EmailIsAlreadyExistedException : CustomException
    {
        public EmailIsAlreadyExistedException()
            : base($"Email is already existed.") { }
    }

    public class PhoneIsAlreadyExistedException : CustomException
    {
        public PhoneIsAlreadyExistedException()
            : base($"Phone is already existed.") { }
    }

    public class InternalServerErrorException : CustomException
    {
        public InternalServerErrorException()
            : base($"Something wrong with the server, please try later.", (int)HttpStatusCode.InternalServerError) { }
    }

    public class InvalidTokenException : CustomException
    {
        public InvalidTokenException()
            : base($"InvalidToken", (int)HttpStatusCode.Unauthorized) { }
    }

    public class NotFoundException : CustomException
    {
        public NotFoundException()
            : base($"Not found", (int)HttpStatusCode.NotFound) { }
    }

    public class InvalidOtpException : CustomException
    {
        public InvalidOtpException()
            : base($"Invalid OTP", (int)HttpStatusCode.BadRequest) { }
    }

    public class InvalidRefreshTokenException : CustomException
    {
        public InvalidRefreshTokenException()
            : base($"Invalid refresh token", (int)HttpStatusCode.BadRequest) { }
    }
}
