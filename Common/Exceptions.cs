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
            : base($"Invalid credentials.", (int)HttpStatusCode.Unauthorized) { }
    }

    public class EmailIsAlreadyExistedException : CustomException
    {
        public EmailIsAlreadyExistedException()
            : base($"Email is already existed.", (int)HttpStatusCode.Conflict) { }
    }

    public class PhoneIsAlreadyExistedException : CustomException
    {
        public PhoneIsAlreadyExistedException()
            : base($"Phone is already existed.", (int)HttpStatusCode.Conflict) { }
    }

    public class InternalServerErrorException : CustomException
    {
        public InternalServerErrorException()
            : base($"Something wrong with the server, please try later.", (int)HttpStatusCode.InternalServerError) { }
    }

    public class InvalidTokenException : CustomException
    {
        public InvalidTokenException()
            : base($"Invalid token", (int)HttpStatusCode.Unauthorized) { }
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
            : base($"Invalid refresh token", (int)HttpStatusCode.Unauthorized) { }
    }

    public class UnverifiedEmailException : CustomException
    {
        public UnverifiedEmailException()
            : base($"Email is not verified", (int)HttpStatusCode.Forbidden) { }
    }

    public class UnauthorizedAccessException : CustomException
    {
        public UnauthorizedAccessException()
            : base($"Unauthorized access", (int)HttpStatusCode.Forbidden) { }
    }

    public class BadRequestException : CustomException
    {
        public BadRequestException(string message)
            : base(message, (int)HttpStatusCode.BadRequest) { }
    }
}
