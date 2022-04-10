using System.Net;
using System.Net.Mail;

namespace Uno.AspNetCore.Framework.Services
{
    public class EmailService : IEmailService
    {
        public void Send(string subject, string body)
        {
            Send(ContextSeed.EmailSettings.Email, subject, body);
        }

        public void Send(string toAddress, string subject, string body)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(ContextSeed.EmailSettings.Email);
            message.To.Add(new MailAddress(toAddress));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            smtp.Port = 587;
            smtp.Host = ContextSeed.EmailSettings.Host;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(ContextSeed.EmailSettings.Email, ContextSeed.EmailSettings.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
    }
}
