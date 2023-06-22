using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface ICaLamViecRepositoryAsync: IGenericRepositoryAsync<CaLamViec>
    {
        Task<CaLamViec> S2_GetByIdAsync(int id);
        Task<IReadOnlyList<CaLamViec>> S2_GetPagedReponseAsync(int pageNumber, int pageSize);

        Task<CaLamViec> S2_GetCaLamViecByNhanVienId(Guid nhanVienId);
    }
}
