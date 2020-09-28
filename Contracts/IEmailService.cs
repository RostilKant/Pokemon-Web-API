using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message);
    }
}