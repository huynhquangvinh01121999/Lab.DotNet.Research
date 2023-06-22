using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class DiemDenRepositoryAsync : GenericRepositoryAsync<DiemDen>, IDiemDenRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public DiemDenRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DiemDen> S2_GetByIdAsync(int Id)
        {
            return await _dbContext.Set<DiemDen>().Where(n => n.Id == Id).FirstOrDefaultAsync();
        }
    }
}
