using EsuhaiSchedule.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsuhaiSchedule.Application.IRepositories
{
    public interface ITongHopDuLieuRepositoryAsync
    {
        Task<IReadOnlyList<GetTongHopXetDuyetViewModel>> S2_Job_GetTongHopXetDuyetC1(int nam, int thang);
        Task<IReadOnlyList<GetTongHopXetDuyetViewModel>> S2_Job_GetTongHopXetDuyetC2(int nam, int thang);
    }
}
