using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class TinNhanRepositoryAsync : GenericRepositoryAsync<TinNhan>, ITinNhanRepositoryAsync
    {
        private readonly DbSet<TinNhan> _tinNhans;
        private readonly ApplicationDbContext _dbContext;

        public TinNhanRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _tinNhans = dbContext.Set<TinNhan>();
            _dbContext = dbContext;
        }

        public async Task<TinNhan> S2_GetByIdAsync(Guid id)
        {
            return await _tinNhans.Where(n => n.Deleted != true)
                                  .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IReadOnlyList<TinNhan>> S2_GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _tinNhans.Where(nv => nv.Deleted != true)
                                  .AsNoTracking()
                                  .ToListAsync();
        }
    }
}
