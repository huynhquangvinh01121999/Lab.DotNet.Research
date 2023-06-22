using EsuhaiHRM.Domain.Entities;
using System.Threading.Tasks;

namespace EsuhaiHRM.Application.Interfaces.Repositories
{
    public interface IDiemDenRepositoryAsync : IGenericRepositoryAsync<DiemDen>
    {
        Task<DiemDen> S2_GetByIdAsync(int Id);
    }
}
