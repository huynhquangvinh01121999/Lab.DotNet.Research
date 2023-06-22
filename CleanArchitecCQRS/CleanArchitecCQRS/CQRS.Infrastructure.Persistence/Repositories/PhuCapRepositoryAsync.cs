using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsByNhanVien;
using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsHrView;
using EsuhaiHRM.Application.Features.PhuCaps.Queries.GetPhuCapsNotHrView;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsHrView;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Identity.Contexts;
using EsuhaiHRM.Infrastructure.Identity.Models;
using EsuhaiHRM.Infrastructure.Identity.Seeds;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class PhuCapRepositoryAsync : GenericRepositoryAsync<PhuCap>, IPhuCapRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private int _totalItem = 0;

        public PhuCapRepositoryAsync(ApplicationDbContext dbContext, IdentityContext idenContext,
            UserManager<ApplicationUser> userManager) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetTotalItem()
        {
            return _totalItem;
        }

        public async Task<PhuCap> S2_GetByGuidAsync(Guid Id)
        {
            return await _dbContext.PhuCaps.Where(n => n.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<PhuCap>> S2_GetPhuCapByMonth(int pageNumber, int pageSize, Guid nhanvienId, DateTime time)
        {
            var tc = _dbContext.PhuCaps.Where(n => n.NhanVienId == nhanvienId &&
                                    n.ThoiGianBatDau.Year == time.Year &&
                                    n.ThoiGianBatDau.Month == time.Month)
                   .OrderByDescending(n => n.ThoiGianBatDau);

            this._totalItem = tc.Count();

            return await tc
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Include(n => n.NhanVien)
                    .Include(m => m.LoaiPhuCap)
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<IReadOnlyList<PhuCap>> S2_GetPagedReponseAsync(int pageNumber, int pageSize, int? phongId, int? banId)
        {
            var phuCaps = from nv in _dbContext.NhanViens
                          join pc in _dbContext.PhuCaps
                          on nv.Id equals pc.NhanVienId
                          where (nv.PhongId == phongId || phongId == null) &&
                                (nv.BanId == banId || banId == null)
                          select pc;

            this._totalItem = phuCaps.Count();

            return await phuCaps
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .Include(n => n.NhanVien)
                                .Include(m => m.LoaiPhuCap)
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async Task<PhuCap> AddNewPhuCapsAsync(PhuCap entity)
        {
            return await AddAsync(ProcessAllowance(entity));
        }

        public async Task<PhuCap> AddNewPhuCapsAsync(PhuCap entity, string loaiPhuCapCode)
        {
            return await AddAsync(ProcessAllowance(TinhSoLanPhuCap(entity, loaiPhuCapCode)));
        }

        public async Task UpdatePhuCapsAsync(PhuCap entity)
        {
            await UpdateAsync(ProcessAllowance(entity));
        }
        public async Task UpdatePhuCapsAsync(PhuCap entity, string loaiPhuCapCode)
        {
            await UpdateAsync(ProcessAllowance(TinhSoLanPhuCap(entity, loaiPhuCapCode)));
        }

        public async Task<IReadOnlyList<GetPhuCapsHrViewModel>> S2_GetAllPhuCapHrView(int pageNumber,
                                                                                        int pageSize,
                                                                                        int phongId,
                                                                                        int banId,
                                                                                        string trangThai,
                                                                                        string keyword,
                                                                                        DateTime thoiGianBatDau,
                                                                                        DateTime thoiGianKetThuc)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@pageNumber",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageNumber},
                    new SqlParameter("@pageSize",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageSize},
                    new SqlParameter("@phongId",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = phongId},
                    new SqlParameter("@banId",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = banId},
                    new SqlParameter("@trangThai",SqlDbType.VarChar) {Direction = ParameterDirection.Input, Value = trangThai},
                    new SqlParameter("@keyword",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(keyword) ? string.Empty: keyword},
                    new SqlParameter("@thoiGianBatDau",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thoiGianBatDau},
                    new SqlParameter("@thoiGianKetThuc",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thoiGianKetThuc},
                    new SqlParameter("@TotalItems",SqlDbType.Int) {Direction = ParameterDirection.InputOutput, Value = 0}
                };

                string sql = string.Format("[{0}].[{1}] @pageNumber, @pageSize, @phongId, @banId, @trangThai, @keyword, @thoiGianBatDau, @thoiGianKetThuc, @TotalItems output", Schemas.NHANSU, Procedures.SP_GetPhuCapsHrView);

                //var phuCaps = await _dbContext.Set<GetPhuCapsHrViewModel>()
                //                                .FromSqlRaw(sql.ToString(), parameter)
                //                                .ToListAsync();

                //this._totalItem = Convert.ToInt32(parameter[8].Value);

                //return phuCaps;

                var result = await _dbContext.Set<GetTimesheetsHrViewResults>()
                                            .FromSqlRaw(sql.ToString(), parameter)
                                            .ToListAsync();

                this._totalItem = Convert.ToInt32(parameter[8].Value);

                if (result.FirstOrDefault() != null)
                    return JsonSerializer.Deserialize<IReadOnlyList<GetPhuCapsHrViewModel>>(result.FirstOrDefault().Result);

                return null;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<GetPhuCapsNotHrViewModel>> S2_GetAllPhuCapNotHrView(int pageNumber,
                                                                                                int pageSize,
                                                                                                Guid nhanVienId,
                                                                                                DateTime thoiGianBatDau,
                                                                                                DateTime thoiGianKetThuc,
                                                                                                string trangThai,
                                                                                                string keyword)
        {

            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@pageNumber",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageNumber},
                    new SqlParameter("@pageSize",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageSize},
                    new SqlParameter("@nhanvienId",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = nhanVienId.ToString()},
                    new SqlParameter("@thoigianbatdau",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thoiGianBatDau},
                    new SqlParameter("@thoigianketthuc",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thoiGianKetThuc},
                    new SqlParameter("@trangThai",SqlDbType.VarChar) {Direction = ParameterDirection.Input, Value = trangThai},
                    new SqlParameter("@keyword",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(keyword) ? string.Empty: keyword},
                    new SqlParameter("@TotalItems",SqlDbType.Int) {Direction = ParameterDirection.InputOutput, Value = 0}
                };

                string sql = string.Format("[{0}].[{1}] @pageNumber, @pageSize, @nhanvienId, @thoigianbatdau, @thoigianketthuc, @trangThai, @keyword, @TotalItems output", Schemas.NHANSU, Procedures.SP_GetPhuCapsNotHrView);

                var result = await _dbContext.Set<GetTimesheetsHrViewResults>()
                                                .FromSqlRaw(sql.ToString(), parameter)
                                                .ToListAsync();

                this._totalItem = Convert.ToInt32(parameter[7].Value);

                if (result.FirstOrDefault() != null)
                    return JsonSerializer.Deserialize<IReadOnlyList<GetPhuCapsNotHrViewModel>>(result.FirstOrDefault().Result);

                return null;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IReadOnlyList<GetPhuCapsByNhanVienViewModel>> S2_GetPhuCapByNhanVien(DateTime thang, Guid nhanvienId)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@thang",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thang},
                    new SqlParameter("@nhanVienId",SqlDbType.UniqueIdentifier) {Direction = ParameterDirection.Input, Value = nhanvienId}
                };

                string sql = string.Format("[{0}].[{1}] @thang, @nhanVienId", Schemas.NHANSU, Procedures.SP_GetPhuCapsByNhanVien);

                var phucaps = await _dbContext.Set<GetPhuCapsByNhanVienViewModel>()
                                            .FromSqlRaw(sql.ToString(), parameter)
                                            .ToListAsync();

                this._totalItem = phucaps.Count();

                return phucaps;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        #region hàm tính số buổi công tác
        private PhuCap ProcessAllowance(PhuCap pc)
        {
            var loaiPhuCap = _dbContext.LoaiPhuCaps.Find(pc.LoaiPhuCapId);
            if (loaiPhuCap.Code.Equals("DICONGTAC"))
            {
                pc.SoBuoiSang = null;
                pc.SoBuoiTrua = null;
                pc.SoBuoiChieu = null;
                pc.SoQuaDem = null;

                if (pc.ThoiGianBatDau.Hour < 7 && pc.ThoiGianKetThuc.Hour < 12)
                    pc.SoBuoiSang = 1;

                if (pc.ThoiGianBatDau.Hour >= 7 && pc.ThoiGianKetThuc.Hour > 12)
                    pc.SoBuoiTrua = 1;

                if (pc.ThoiGianBatDau.Hour < 7 && pc.ThoiGianKetThuc.Hour > 12)
                    pc.SoBuoiChieu = 1;

                TimeSpan ts = pc.ThoiGianKetThuc.Subtract(pc.ThoiGianBatDau);

                if (ts.Days > 0)
                    pc.SoQuaDem = (short)ts.Days;
            }

            return pc;
        }
        #endregion

        #region hàm tính số lần phụ cấp
        private PhuCap TinhSoLanPhuCap(PhuCap pc, string loaiPhuCapCode)
        {
            switch (loaiPhuCapCode)
            {
                case "DICONGTAC":
                    pc.SoLanPhuCap = null;
                    break;
                default:
                    pc.SoLanPhuCap = 1;
                    break;
            }
            return pc;
        }
        #endregion
    }
}
