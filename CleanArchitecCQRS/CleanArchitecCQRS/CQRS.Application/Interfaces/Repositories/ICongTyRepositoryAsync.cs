using EsuhaiHRM.Application.Features.CongTys.Queries.GetAllCongTys;
using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface ICongTyRepositoryAsync: IGenericRepositoryAsync<CongTy>
    {
        Task<CongTy> S2_GetByIdAsync(int id);
        Task<IReadOnlyList<CongTy>> S2_GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<IReadOnlyList<GetAllCongTysViewModel>> S2_GetPagedReponseAsyncWithSearch(int pageNumber, int pageSize, string searchValue);
        int GetToTalItemReponse();
    }
}
