namespace Uno.AspNetCore.Framework.Services
{
    public interface IEmailService
    {
        void Send(string subject, string body);
        void Send(string toAddress, string subject, string body);
    }
}
