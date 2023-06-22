using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class DanhMucRepositoryAsync : GenericRepositoryAsync<DanhMuc>, IDanhMucRepositoryAsync
    {
        private readonly DbSet<DanhMuc> _danhMucs;

        public DanhMucRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _danhMucs = dbContext.Set<DanhMuc>();
        }

        public async Task<DanhMuc> S2_GetByIdAsync(int id)
        {
            return await _danhMucs.Where(n => n.Deleted != true)
                                  .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IReadOnlyList<DanhMuc>> S2_GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _danhMucs.Where(n => n.Deleted != true)
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .AsNoTracking()
                                  .ToListAsync();
        }
    }
}
