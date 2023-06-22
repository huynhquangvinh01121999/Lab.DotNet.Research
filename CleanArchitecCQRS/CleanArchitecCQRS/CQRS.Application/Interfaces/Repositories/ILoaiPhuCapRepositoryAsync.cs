using EsuhaiHRM.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface ILoaiPhuCapRepositoryAsync : IGenericRepositoryAsync<LoaiPhuCap>
    {
        Task<LoaiPhuCap> S2_GetByIdAsync(int Id);
        Task<IReadOnlyList<LoaiPhuCap>> S2_GetAllLoaiPhuCap();
    }
}
