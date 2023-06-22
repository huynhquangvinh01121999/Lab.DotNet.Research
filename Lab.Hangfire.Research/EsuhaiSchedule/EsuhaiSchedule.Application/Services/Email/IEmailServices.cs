using EsuhaiSchedule.Application.DTOs;
using System.Threading.Tasks;

namespace EsuhaiSchedule.Application.Services.Email
{
    public interface IEmailServices
    {
        Task SendAsync(EmailDtos request);
    }
}
