using Application.DTOs.Email;
using System.Threading.Tasks;

namespace Application.Features.Emails
{
    public interface IEmailServices
    {
        Task SendAsync(EmailRequest request);
    }
}
