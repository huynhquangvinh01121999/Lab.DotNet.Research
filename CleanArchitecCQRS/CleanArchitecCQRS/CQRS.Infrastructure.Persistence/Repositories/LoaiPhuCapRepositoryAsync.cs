using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class LoaiPhuCapRepositoryAsync : GenericRepositoryAsync<LoaiPhuCap>, ILoaiPhuCapRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public LoaiPhuCapRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LoaiPhuCap> S2_GetByIdAsync(int Id) => await _dbContext.LoaiPhuCaps.Where(x => x.Id == Id).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<LoaiPhuCap>> S2_GetAllLoaiPhuCap()
        {
            var results = await _dbContext.LoaiPhuCaps.ToListAsync();
            return results.Where(x => x.Deleted != true).ToList();
        }
    }
}
