using lab.hangfire.net.Models;
using System.Threading.Tasks;

namespace lab.hangfire.net.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
