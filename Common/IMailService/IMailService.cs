namespace f00die_finder_be.Common.IMailService
{
    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string templatePath, object data);

    }
}
