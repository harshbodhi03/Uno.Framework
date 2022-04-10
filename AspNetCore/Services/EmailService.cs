using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Uno.AspNetCore.Framework.Services
{
    public class EmailService : IEmailService
    {
        private string mAdminAddress;
        private string mPassword;
        private string mHostAddress;
        public EmailService(IConfiguration configuration)
        {
            mAdminAddress = configuration["Moderator:EmailSettings:Email"];
            mPassword = configuration["Moderator:EmailSettings:Password"];
            mHostAddress = configuration["Moderator:EmailSettings:Host"];
        }

        public void Send(string subject, string body)
        {
            Send(mAdminAddress, subject, body);
        }

        public void Send(string toAddress, string subject, string body)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(mAdminAddress);
            message.To.Add(new MailAddress(toAddress));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            smtp.Port = 587;
            smtp.Host = mHostAddress;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(mAdminAddress, mPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
    }
}
