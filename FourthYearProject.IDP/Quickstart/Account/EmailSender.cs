using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace FourthYearProject
{
    public class EmailSender : IEmailSender
    {
        public EmailSender()
        {
        }


        public Task SendEmailAsync(string email, string subject, string message)
        {
            var ApiKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("EmailSettings")["APIKey"];
            return Execute(ApiKey, subject, message, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("cdrgamescdr@gmail.com", "Fotostop"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
