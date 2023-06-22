using EsuhaiHRM.Application.Features.KhoaDaoTaos.Queries.GetAllKhoaDaoTaos;
using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface IKhoaDaoTaoRepositoryAsync : IGenericRepositoryAsync<KhoaDaoTao>
    {
        Task<KhoaDaoTao> S2_GetByIdAsync(int id);
        Task<IReadOnlyList<GetAllKhoaDaoTaosViewModel>> S2_GetPagedReponseAsync(int pageNumber, int pageSize);
    }
}
