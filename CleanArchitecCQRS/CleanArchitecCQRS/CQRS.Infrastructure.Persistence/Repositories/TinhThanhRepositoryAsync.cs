using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class TinhThanhRepositoryAsync : GenericRepositoryAsync<TinhThanh>, ITinhThanhRepositoryAsync
    {
        private readonly DbSet<TinhThanh> _tinhThanhs;
        private readonly DbSet<Huyen> _huyens;
        private readonly DbSet<Xa> _xas;

        public TinhThanhRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _tinhThanhs = dbContext.Set<TinhThanh>();
            _huyens = dbContext.Set<Huyen>();
            _xas = dbContext.Set<Xa>();
        }
        public async Task<TinhThanh> S2_GetByIdAsync(int id)
        {
            return await _tinhThanhs.Where(n => n.Deleted != true)
                                    .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IReadOnlyList<TinhThanh>> S2_GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _tinhThanhs.Where(n => n.Deleted != true)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .AsNoTracking()
                                    .ToListAsync();
        }

        public async Task<IEnumerable<Huyen>> S2_GetHuyenByTinhIdAsync(int pageNumber, int pageSize, int tinhId)
        {
            return await _huyens.Where(hu => hu.Deleted != true && hu.TinhId == tinhId)
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async Task<IEnumerable<Xa>> S2_GetXaByHuyenIdAsync(int pageNumber, int pageSize, int huyenId)
        {
            return await _xas.Where(hu => hu.Deleted != true && hu.HuyenId == huyenId)
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .AsNoTracking()
                             .ToListAsync();
        }
    }
}
