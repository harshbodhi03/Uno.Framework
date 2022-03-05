using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Uno.Framework.Services
{
	public class EmailService : IEmailService
	{
		private string mAdminAddress;
		private string mPassword;
		private string mHostAddress;
		public EmailService(IConfiguration configuration)
		{
			mAdminAddress = configuration["Moderator:EmailConfig:Email"];
			mPassword = configuration["Moderator:EmailConfig:Password"];
			mHostAddress = configuration["Moderator:EmailConfig:Host"];
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
