using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace Booksy.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("mohamedashrafmahmoudgad@gmail.com", "iztl wbgr njbi xgux")
            };

            return client.SendMailAsync(
            new MailMessage(from: "your.email@live.com",
                            to: email,
                            subject,
                            htmlMessage
                            )
            {
                IsBodyHtml = true
            });
        }
    }
}
