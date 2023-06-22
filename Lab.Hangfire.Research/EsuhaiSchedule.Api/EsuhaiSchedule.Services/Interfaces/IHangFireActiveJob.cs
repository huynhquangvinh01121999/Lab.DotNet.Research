using Hangfire;
using System.Threading.Tasks;

namespace EsuhaiSchedule.Services.Interfaces
{
    public interface IHangFireActiveJob
    {
        Task Run(IJobCancellationToken token);
        Task RunAtTimeOf();
    }
}
