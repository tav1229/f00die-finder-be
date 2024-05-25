namespace f00die_finder_be.Common
{
    public static class MailConsts
    {
        public static class ForgotPassword
        {
            public const string Subject = "[f00diefinder.id.vn] Forgot password";
            public const string Template = "ForgotPassword.html";
        }

        public static class VerifyEmail
        {
            public const string Subject = "[f00diefinder.id.vn] Verify email";
            public const string Template = "VerifyEmail.html";
        }
    }
}
