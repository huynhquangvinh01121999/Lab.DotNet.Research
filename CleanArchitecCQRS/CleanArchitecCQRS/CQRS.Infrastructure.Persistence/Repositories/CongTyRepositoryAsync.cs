using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EsuhaiHRM.Application.Features.CongTys.Queries.GetAllCongTys;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class CongTyRepositoryAsync : GenericRepositoryAsync<CongTy>, ICongTyRepositoryAsync
    {
        private readonly DbSet<CongTy> _congTys;
        private readonly ApplicationDbContext _dbContext;
        private int _totalItems = 0; //Add _totalItem to count all items of GET request 

        public CongTyRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _congTys = dbContext.Set<CongTy>();
            _dbContext = dbContext;
        }

        public async Task<CongTy> S2_GetByIdAsync(int id)
        {
            return await _congTys.Where(n => n.Deleted != true)
                                 .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IReadOnlyList<CongTy>> S2_GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _congTys.Where(ct => ct.Deleted != true)
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<IReadOnlyList<GetAllCongTysViewModel>> S2_GetPagedReponseAsyncWithSearch(int pageNumber, int pageSize, string searchValue)
        {
            if (searchValue == null)
            {
                var nhanvien_congtys = _dbContext.NhanVien_CongTys
                                                 .Where(nc => nc.Deleted != true)
                                                 .GroupBy(nc => nc.CongTyId)
                                                 .Select(snc => new { CongtyId = snc.Key, count = snc.Count()});
                var results = from ct in _congTys where ct.Deleted != true
                              join nc in nhanvien_congtys on ct.Id equals nc.CongtyId into leftjoin
                              from lf in leftjoin.DefaultIfEmpty()
                              select new GetAllCongTysViewModel
                              {
                                Id = ct.Id,
                                Code = ct.Code,
                                MaSoThue = ct.MaSoThue,
                                TenCongTyVN = ct.TenCongTyVN,
                                TenCongTyEN = ct.TenCongTyEN,
                                TenCongTyJP = ct.TenCongTyJP,
                                TenVietTat = ct.TenVietTat,
                                TenGiamDoc = ct.TenGiamDoc,
                                TrangThai = ct.TrangThai,
                                GhiChu = ct.GhiChu,
                                TongNhanVien = (lf == null ? 0 : lf.count),
                              };
                _totalItems = results.Count(); //set count total items to _totalItem

                return await results.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .AsNoTracking()
                                    .ToListAsync();
            }
            else
            {
                var nhanvien_congtys = _dbContext.NhanVien_CongTys
                                                 .Where(nc => nc.Deleted != null)
                                                 .GroupBy(nc => nc.CongTyId)
                                                 .Select(snc => new { CongtyId = snc.Key, count = snc.Count() });
                var results = from ct in _congTys.Where(ct => ct.TenCongTyVN.Contains(searchValue)
                                                           || ct.TenCongTyEN.Contains(searchValue)
                                                           || ct.TenCongTyJP.Contains(searchValue))
                                                 .Where(ct => ct.Deleted != null)
                              join nc in nhanvien_congtys on ct.Id equals nc.CongtyId into leftjoin
                              from lf in leftjoin.DefaultIfEmpty()
                              select new GetAllCongTysViewModel
                              {
                                Id = ct.Id,
                                Code = ct.Code,
                                MaSoThue = ct.MaSoThue,
                                TenCongTyVN = ct.TenCongTyVN,
                                TenCongTyEN = ct.TenCongTyEN,
                                TenCongTyJP = ct.TenCongTyJP,
                                TenVietTat = ct.TenVietTat,
                                TenGiamDoc = ct.TenGiamDoc,
                                TrangThai = ct.TrangThai,
                                GhiChu = ct.GhiChu,
                                TongNhanVien = (lf == null ? 0 : lf.count),
                              };

                _totalItems = results.Count(); //set count total items to _totalItem

                return await results.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .AsNoTracking()
                                    .ToListAsync();
            }
        }
        /// <summary>
        /// Function return count all iteam after GET request
        /// </summary>
        /// <returns></returns>
        public int GetToTalItemReponse()
        {
            return _totalItems;
        }
    }
}
