using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface ITinhThanhRepositoryAsync: IGenericRepositoryAsync<TinhThanh>
    {
        Task<TinhThanh> S2_GetByIdAsync(int id);
        Task<IReadOnlyList<TinhThanh>> S2_GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Huyen>> S2_GetHuyenByTinhIdAsync(int pageNumber, int pageSize, int tinhId);
        Task<IEnumerable<Xa>> S2_GetXaByHuyenIdAsync(int pageNumber, int pageSize, int huyenId);
    }
}
