using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface ITinNhanRepositoryAsync: IGenericRepositoryAsync<TinNhan>
    {
        Task<TinNhan> S2_GetByIdAsync(Guid id);
        Task<IReadOnlyList<TinNhan>> S2_GetPagedReponseAsync(int pageNumber, int pageSize);
    }
}
