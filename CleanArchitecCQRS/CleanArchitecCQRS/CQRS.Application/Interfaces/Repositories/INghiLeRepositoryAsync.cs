using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface INghiLeRepositoryAsync : IGenericRepositoryAsync<NghiLe>
    {
        Task<int> GetTotalItem();
        Task<IEnumerable<NghiLe>> S2_GetNghiLesAsync(int nam);
        Task<NghiLe> S2_GetNghiLeByIdAsync(int id);
        Task<bool> S2_isExistNghiLe(DateTime? ngay, DateTime? ngayCoDinh);
    }
}
