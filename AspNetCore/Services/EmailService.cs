using System.Net;
using System.Net.Mail;

namespace Uno.AspNetCore.Framework.Services
{
	public class EmailService : IEmailService
	{
		private readonly IContextSeedService _contextSeedService;

		public EmailService(IContextSeedService contextSeedService)
		{
			_contextSeedService = contextSeedService;
		}

		/// <summary>
		/// Send email to self.
		/// </summary>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		public void Send(string subject, string body)
		{
			Send(_contextSeedService.EmailSettings.Email, subject, body);
		}

		/// <summary>
		/// Send email to provided address.
		/// </summary>
		/// <param name="toAddress"></param>
		/// <param name="subject"></param>
		/// <param name="body"></param>
		public void Send(string toAddress, string subject, string body)
		{
			MailMessage message = new MailMessage();
			SmtpClient smtp = new SmtpClient();
			message.From = new MailAddress(_contextSeedService.EmailSettings.Email);
			message.To.Add(new MailAddress(toAddress));
			message.Subject = subject;
			message.Body = body;
			message.IsBodyHtml = true;
			smtp.Port = 587;
			smtp.Host = _contextSeedService.EmailSettings.Host;
			smtp.EnableSsl = true;
			smtp.UseDefaultCredentials = false;
			smtp.Credentials = new NetworkCredential(_contextSeedService.EmailSettings.Email, _contextSeedService.EmailSettings.Password);
			smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtp.Send(message);
		}
	}
}
