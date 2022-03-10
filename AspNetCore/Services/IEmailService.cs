namespace Uno.AspNetCore.Framework.Services
{
    public interface IEmailService
    {
        void Send(string toAddress, string subject, string body);
    }
}
