using EsuhaiSchedule.Application.DTOs;
using EsuhaiSchedule.Application.Wrappers;
using System.Threading.Tasks;

namespace EsuhaiSchedule.Application.Services.RecurringJob
{
    public interface IRecurringJobService
    {
        Task S2_SendMail_TongHopXetDuyetC1(int nam, int thang);
        Task S2_SendMail_TongHopXetDuyetC2(int nam, int thang);

        Response<Task> S2_AddOrUpdateRecurringJob(AddOrUpdateDtos request);
    }
}
