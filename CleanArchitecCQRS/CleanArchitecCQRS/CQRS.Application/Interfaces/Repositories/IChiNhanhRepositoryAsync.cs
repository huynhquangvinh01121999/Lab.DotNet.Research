using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface IChiNhanhRepositoryAsync: IGenericRepositoryAsync<ChiNhanh>
    {
        Task<ChiNhanh> S2_GetByIdAsync(Guid id);
        Task<IReadOnlyList<ChiNhanh>> S2_GetPagedReponseAsync(int pageNumber, int pageSize);
    }
}
