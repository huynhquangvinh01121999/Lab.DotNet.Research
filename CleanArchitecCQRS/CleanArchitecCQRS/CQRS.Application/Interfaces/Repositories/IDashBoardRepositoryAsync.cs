using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface IDashBoardRepositoryAsync : IGenericRepositoryAsync<DashBoard>
    {
        Task<DashBoard> S2_GetDashBoardAsync();
        Task<IEnumerable<DashBoard_12Months>> S2_GetListNhanVienInMonths(int? Year);
    }
}
