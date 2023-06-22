using EsuhaiHRM.Application.Features.ChucVus.Queries.GetAllChucVus;
using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface IChucVuRepositoryAsync: IGenericRepositoryAsync<ChucVu>
    {
        Task<ChucVu> S2_GetByIdAsync(int id);
        Task<IReadOnlyList<GetAllChucVusViewModel>> S2_GetPagedReponseAsync(int pageNumber, int pageSize);
    }
}
