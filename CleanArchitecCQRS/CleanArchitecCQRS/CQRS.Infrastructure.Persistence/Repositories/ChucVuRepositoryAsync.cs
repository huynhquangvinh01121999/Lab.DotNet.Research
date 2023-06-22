using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EsuhaiHRM.Application.Features.ChucVus.Queries.GetAllChucVus;
using System.Linq;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class ChucVuRepositoryAsync : GenericRepositoryAsync<ChucVu>, IChucVuRepositoryAsync
    {
        private readonly DbSet<ChucVu> _chucVus;
        private readonly ApplicationDbContext _dbContext;

        public ChucVuRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _chucVus = dbContext.Set<ChucVu>();
            _dbContext = dbContext;
        }

        public async Task<ChucVu> S2_GetByIdAsync(int id)
        {
            return await _chucVus.Where(n => n.Deleted != true)
                                 .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IReadOnlyList<GetAllChucVusViewModel>> S2_GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            var listCountChucVu = _dbContext.NhanViens
                                            .Where(nv => nv.ChucVuId != null && nv.Deleted != true)
                                            .GroupBy(nv => nv.ChucVuId)
                                            .Select(snv => new { ChucVuId = snv.Key, count = snv.Count() })
                                            .Union(_dbContext.NhanViens
                                                             .Where(nv => nv.ChucDanhId != null && nv.Deleted != true)
                                                             .GroupBy(nv => nv.ChucDanhId)
                                                             .Select(snv => new { ChucVuId = snv.Key, count = snv.Count()}));
            var results = from cv in _chucVus where cv.Deleted != true
                          join nc in listCountChucVu on cv.Id equals nc.ChucVuId into leftjoin
                          from lf in leftjoin.DefaultIfEmpty()
                          select new GetAllChucVusViewModel
                          {
                              Id = cv.Id,
                              TenVN = cv.TenVN,
                              TenEN = cv.TenEN,
                              TenJP = cv.TenJP,
                              CapBac = cv.CapBac,
                              PhanLoai = cv.PhanLoai,
                              GhiChu = cv.GhiChu,
                              TongNhanVien = (lf == null ? 0 : lf.count),
                          };
            return await results.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .AsNoTracking()
                                .ToListAsync();
        }
    }
}
