using EsuhaiHRM.Application.Features.PhongBans.Queries.GetAllPhongBans;
using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface IPhongBanRepositoryAsync: IGenericRepositoryAsync<PhongBan>
    {
        Task<PhongBan> S2_GetByIdAsync(int id);
        Task<IReadOnlyList<PhongBan>> S2_GetPagedReponseAsyncDropDown(int pageNumber, int pageSize);
        Task<IReadOnlyList<GetAllPhongBansViewModel>> S2_GetPagedReponseAsync(int pageNumber, int pageSize); 
    }
}
