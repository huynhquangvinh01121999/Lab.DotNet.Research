using EsuhaiHRM.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface ICauHinhNgayCongRepositoryAsync : IGenericRepositoryAsync<CauHinhNgayCong>
    {
        Task<int> GetTotalItem();
        Task<IEnumerable<CauHinhNgayCong>> S2_GetCauHinhNgayCongsAsync(int nam);
        Task<CauHinhNgayCong> S2_GetCauHinhNgayCongByIdAsync(int id);
        Task<CauHinhNgayCong> S2_GetCauHinhNgayCongByDate(int? thang, int? nam);
    }
}
