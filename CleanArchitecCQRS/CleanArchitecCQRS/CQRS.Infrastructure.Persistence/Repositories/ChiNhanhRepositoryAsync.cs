using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class ChiNhanhRepositoryAsync : GenericRepositoryAsync<ChiNhanh>, IChiNhanhRepositoryAsync
    {
        private readonly DbSet<ChiNhanh> _chiNhanhs;

        public ChiNhanhRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _chiNhanhs = dbContext.Set<ChiNhanh>();
        }

        public async Task<ChiNhanh> S2_GetByIdAsync(Guid id)
        {
            return await _chiNhanhs.Where(cn => cn.Deleted != true)
                                   .FirstOrDefaultAsync(cn => cn.Id == id);
        }

        public async Task<IReadOnlyList<ChiNhanh>> S2_GetPagedReponseAsync (int pageNumber, int pageSize)
        {
            return await _chiNhanhs.Where(cn => cn.Deleted != true)
                                   .Include(cn => cn.LoaiChiNhanh)
                                   .Include(cn => cn.TinhThanh)
                                   .Include(cn => cn.TruSoChinh)
                                   .Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .AsNoTracking()
                                   .ToListAsync();
        }



    }
}
