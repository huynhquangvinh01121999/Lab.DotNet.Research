using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EsuhaiHRM.Application.Features.GroupMails.Queries.GetAllGroupMails;
using System.Linq;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class GroupMailRepositoryAsync : GenericRepositoryAsync<GroupMail>, IGroupMailRepositoryAsync
    {
        private readonly DbSet<GroupMail> _groupMails;
        private readonly DbSet<NhanVien> _nhanViens;
        private readonly ApplicationDbContext _dbContext;

        public GroupMailRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _groupMails = dbContext.Set<GroupMail>();
            _nhanViens = dbContext.Set<NhanVien>();
            _dbContext = dbContext;
        }

        public async Task<GroupMail> S2_GetByIdAsync(int id)
        {
            return await _groupMails.Where(n => n.Deleted != true)
                                    .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IReadOnlyList<GetAllGroupMailsViewModel>> S2_GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            var nhanvien_groupmails = _dbContext.NhanVien_GroupMails
                                                .Where(nv => nv.Deleted != true)
                                                .GroupBy(nv => nv.GroupMailId)
                                                .Select(snc => new { GroupMailID = snc.Key, count = snc.Count() });
            var results = from ct in _groupMails where ct.Deleted != true
                          join nc in nhanvien_groupmails on ct.Id equals nc.GroupMailID into leftjoin
                          from lf in leftjoin.DefaultIfEmpty()
                          select new GetAllGroupMailsViewModel
                          {
                              Id = ct.Id,
                              Ten = ct.Ten,
                              NhanVienDeNghiId = ct.NhanVienDeNghiId,
                              NhanVienDeNghiHoTen = (from nv in _nhanViens
                                                     where nv.Id == ct.NhanVienDeNghiId
                                                     select string.Format("{0} {1}", nv.HoTenDemVN, nv.TenVN))
                                                     .FirstOrDefault(),
                              NgayTao = ct.NgayTao,
                              MucDich = ct.MucDich,
                              MoTa = ct.MoTa,
                              PhanLoai = ct.PhanLoai,
                              GhiChu = ct.GhiChu,
                              TongNhanVien = (lf == null ? 0 : lf.count),
                              NhanViens = (from nvgm in ct.NhanVien_GroupMails
                                           join nv in _dbContext.NhanViens
                                           on nvgm.NhanVienId equals nv.Id
                                           join ph in _dbContext.PhongBans
                                           on nv.PhongId equals ph.Id
                                           join bn in _dbContext.PhongBans
                                           on nv.BanId equals bn.Id
                                           select new GetAllGroupMailNhanViensViewModel
                                           {
                                               Id = nv.Id,
                                               MaNhanVien = nv.MaNhanVien,
                                               HoTenVN = string.Format("{0} {1}", nv.HoTenDemVN, nv.TenVN),
                                               PhongTenVN = ph.TenVN,
                                               PhongTenJP = ph.TenJP,
                                               BanTenVN = bn.TenVN,
                                               BanTenJP = bn.TenJP,
                                               GhiChu = nvgm.GhiChu,
                                           }).ToList()
                          };
            return await results.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .AsNoTracking()
                                .ToListAsync();
        }
    }
}
