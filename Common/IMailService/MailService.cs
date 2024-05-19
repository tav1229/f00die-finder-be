using HandlebarsDotNet;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace f00die_finder_be.Common.IMailService
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string templatePath, object data)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["Mail:Email"]));
            email.To.Add(MailboxAddress.Parse(toEmail));

            email.Subject = subject;

            templatePath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["TemplateRelativePath"], templatePath);
            var body = RenderTemplate(templatePath, data);
            email.Body = new TextPart("html") { Text = body };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration["Mail:Host"], int.Parse(_configuration["Mail:Port"]), SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_configuration["Mail:Email"], _configuration["Mail:Password"]);
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
        }

        public string RenderTemplate(string templatePath, object data)
        {
            var source = File.ReadAllText(templatePath);

            var template = Handlebars.Compile(source);

            var result = template(data);

            return result;
        }
    }
}
