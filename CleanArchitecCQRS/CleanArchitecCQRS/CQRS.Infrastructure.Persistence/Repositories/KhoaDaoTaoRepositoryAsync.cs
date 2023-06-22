using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EsuhaiHRM.Application.Features.KhoaDaoTaos.Queries.GetAllKhoaDaoTaos;
using System.Linq;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class KhoaDaoTaoRepositoryAsync : GenericRepositoryAsync<KhoaDaoTao>, IKhoaDaoTaoRepositoryAsync
    {
        private readonly DbSet<KhoaDaoTao> _khoaDaoTaos;
        private readonly ApplicationDbContext _dbContext;

        public KhoaDaoTaoRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _khoaDaoTaos = dbContext.Set<KhoaDaoTao>();
            _dbContext = dbContext;
        }

        public async Task<KhoaDaoTao> S2_GetByIdAsync(int id)
        {
            return await _khoaDaoTaos.Where(n => n.Deleted != true)
                                     .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IReadOnlyList<GetAllKhoaDaoTaosViewModel>> S2_GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            var nhanvien_khoadaotaos = _dbContext.NhanVien_KhoaDaoTaos
                                                 .Where(nvkdt => nvkdt.Deleted != true)
                                                 .GroupBy(nvkdt => nvkdt.KhoaDaoTaoId)
                                                 .Select(nvkdt => new { KhoaDaoTaoId = nvkdt.Key, count = nvkdt.Count() });

            var results = from kdt in _khoaDaoTaos where kdt.Deleted != true
                          join nv_kdt in nhanvien_khoadaotaos on kdt.Id equals nv_kdt.KhoaDaoTaoId into leftjoin
                          from lf in leftjoin.DefaultIfEmpty()
                          select new GetAllKhoaDaoTaosViewModel
                          {
                              Id = kdt.Id,
                              TenVN = kdt.TenVN,
                              TenJP = kdt.TenJP,
                              NgayDaoTao = kdt.NgayDaoTao,
                              NgayBatDau = kdt.NgayBatDau,
                              NgayKetThuc = kdt.NgayKetThuc,
                              NguoiDaoTao = kdt.NguoiDaoTao,
                              MucDich = kdt.MucDich,
                              DiaDiem = kdt.DiaDiem,
                              GhiChu = kdt.GhiChu,
                              TongNhanVien = (lf == null ? 0 : lf.count),
                              NhanViens = (from nvkdt in kdt.NhanVien_KhoaDaoTaos
                                           join nv    in _dbContext.NhanViens
                                           on   nvkdt.NhanVienId equals nv.Id
                                           join ph    in _dbContext.PhongBans
                                           on   nv.PhongId equals ph.Id
                                           join bn in _dbContext.PhongBans
                                           on   nv.BanId equals bn.Id
                                           select new GetAllKhoaDaoTaoNhanViensViewModel 
                                           {
                                               Id = nv.Id,
                                               MaNhanVien = nv.MaNhanVien,
                                               HoTenVN = string.Format("{0} {1}",nv.HoTenDemVN,nv.TenVN),
                                               PhongTenVN = ph.TenVN,
                                               PhongTenJP = ph.TenJP,
                                               BanTenVN = bn.TenVN,
                                               BanTenJP = bn.TenJP,
                                               GhiChu = nvkdt.GhiChu,
                                           }).ToList()

                          };
            return await results.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .AsNoTracking()
                                .ToListAsync();
        }
    }
}
