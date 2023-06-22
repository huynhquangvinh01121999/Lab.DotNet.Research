using EsuhaiSchedule.Services.DTOs.Email;
using System.Threading.Tasks;

namespace EsuhaiSchedule.Services.Interfaces
{
    public interface IEmailServices
    {
        Task SendAsync(EmailRequest request);
    }
}
