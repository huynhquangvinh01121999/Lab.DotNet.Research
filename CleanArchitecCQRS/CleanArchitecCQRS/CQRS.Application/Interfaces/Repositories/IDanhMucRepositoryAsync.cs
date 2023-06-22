using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface IDanhMucRepositoryAsync: IGenericRepositoryAsync<DanhMuc>
    {
        Task<DanhMuc> S2_GetByIdAsync(int id);
        Task<IReadOnlyList<DanhMuc>> S2_GetPagedReponseAsync(int pageNumber, int pageSize);
    }
}
