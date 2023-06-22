using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EsuhaiHRM.Application.Features.PhongBans.Queries.GetAllPhongBans;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class PhongBanRepositoryAsync : GenericRepositoryAsync<PhongBan>, IPhongBanRepositoryAsync
    {
        private readonly DbSet<PhongBan> _phongBans;
        private readonly ApplicationDbContext _dbContext;

        public PhongBanRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _phongBans = dbContext.Set<PhongBan>();
            _dbContext = dbContext;
        }

        public async Task<PhongBan> S2_GetByIdAsync(int id)
        {
            return await _phongBans.Where(n => n.Deleted != true)
                                   .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IReadOnlyList<PhongBan>> S2_GetPagedReponseAsyncDropDown(int pageNumber, int pageSize)
        {
            return await _phongBans.Where(p => p.PhanLoai != "nhom" && p.Deleted != true)
                                   .AsNoTracking()
                                   .ToListAsync();
        }

        public async Task<IReadOnlyList<GetAllPhongBansViewModel>> S2_GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            var dsNvPhong = _dbContext.NhanViens.Where(nc => nc.Deleted != true)
                                                .GroupBy(nc => nc.PhongId)
                                                .Select(snc => new { PhongBan = snc.Key, count = snc.Count() });
            var dsNvBan = _dbContext.NhanViens.Where(nc => nc.Deleted != true)
                                              .GroupBy(nc => nc.BanId)
                                              .Select(snc => new { PhongBan = snc.Key, count = snc.Count() });
            var dsNvNhom = _dbContext.NhanViens.Where(nc => nc.Deleted != true)
                                               .GroupBy(nc => nc.NhomId)
                                               .Select(snc => new { PhongBan = snc.Key, count = snc.Count() });
            var dsNvPhongBan = dsNvPhong.Union(dsNvBan).Union(dsNvNhom);

            var results = from pb in _dbContext.PhongBans.Include(pb => pb.GroupMail) where pb.Deleted != true
                          join nv in dsNvPhongBan on pb.Id equals nv.PhongBan into leftjoin
                          from lf in leftjoin.DefaultIfEmpty()
                          select new GetAllPhongBansViewModel
                          {
                              Id = pb.Id,
                              TenVN = pb.TenVN,
                              TenEN = pb.TenEN,
                              TenJP = pb.TenJP,
                              PhanLoai = pb.PhanLoai,
                              TruongBoPhanId = pb.TruongBoPhanId,
                              Parent = pb.Parent,
                              TenGroupMail = pb.GroupMail.Ten,
                              TrangThai = pb.TrangThai,
                              GhiChu = pb.GhiChu,
                              TruongPhongTenVN = pb.TruongBoPhan.TenVN,
                              TruongPhongHoTenDemVN = pb.TruongBoPhan.HoTenDemVN,
                              PhongBanParentTenVN = pb.PhongBanParent.TenVN,
                              PhongBanParentTenJP = pb.PhongBanParent.TenJP,
                              TongNhanVien = (lf == null ? 0 : lf.count),
                          };
            return await results.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .AsNoTracking()
                                .ToListAsync();
        }
    }
}
