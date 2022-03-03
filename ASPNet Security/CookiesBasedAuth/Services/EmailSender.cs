using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace CookiesBasedAuth.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<SmtpSettings> emailOptions;
        public EmailSender(IOptions<SmtpSettings> emailOptions)
        {
            this.emailOptions = emailOptions;

        }
        public async Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message)
        {
            var mail = new MailMessage(fromAddress, toAddress, subject, message);
            using (var client = new SmtpClient(emailOptions.Value.Host, emailOptions.Value.Port)
            {
                Credentials = new NetworkCredential(emailOptions.Value.UserName, emailOptions.Value.Password)
            })
            {
                await client.SendMailAsync(mail);
            }
        }
    }
}