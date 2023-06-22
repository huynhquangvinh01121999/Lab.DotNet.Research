using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EsuhaiHRM.Infrastructure.Identity.Contexts;
using EsuhaiHRM.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using EsuhaiHRM.Application.Features.TongHopDuLieus.Queries.GetTongHopDuLieusByNhanVien;
using EsuhaiHRM.Infrastructure.Identity.Seeds;
using Microsoft.Data.SqlClient;
using System.Data;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsHrView;
using System.Text.Json;
using EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopDuLieuNhanVien;
using EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.QuanLyNgayCong;
using EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopNghi;
using EsuhaiHRM.Application.Features.TongHopDuLieu.Queries.GetTongHopNgayCong;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class TongHopDuLieuRepositoryAsync : GenericRepositoryAsync<TongHopDuLieu>, ITongHopDuLieuRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TongHopDuLieu> _tongHopDuLieus;
        private readonly DbSet<Timesheet> _timeSheets;


        private int _totalItem = 0;

        public TongHopDuLieuRepositoryAsync(ApplicationDbContext dbContext, IdentityContext idenContext, UserManager<ApplicationUser> userManager) : base(dbContext)
        {
            _dbContext = dbContext;
            _timeSheets = dbContext.Set<Timesheet>();
            _tongHopDuLieus = dbContext.Set<TongHopDuLieu>();
        }

        public async Task<TongHopDuLieu> S2_GetByIdAsync(int Id)
        {
            return await _tongHopDuLieus.Where(n => n.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<TongHopDuLieu>> S2_GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            this._totalItem = _tongHopDuLieus.Count();

            return await _tongHopDuLieus
                .Include(n => n.NhanVien)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
        
        public async Task<int> GetTotalItem()
        {
            return _totalItem;
        }

        public async Task<IReadOnlyList<TongHopDuLieu>> S2_GetTimesheetsByNhanVienIdAsync(Guid nhanvienId, int pageNumber, int pageSize)
        {
            var ts = _tongHopDuLieus
                .Where(n => n.NhanVienId == nhanvienId);

            this._totalItem = ts.Count();

            return await ts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(n=>n.NhanVien)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<GetTongHopDuLieusByNhanVienViewModel>> S2_GetTimesheetsInMonth1(Guid nhanvienId, int Thang, int Nam)
        {
            var ts = _tongHopDuLieus
                .Where(n => n.NhanVienId == nhanvienId && n.NgayLamViec.Year == Nam && n.NgayLamViec.Month == Thang)
                .Include(n => n.NhanVien)
                .OrderByDescending(n => n.NgayLamViec);

            var list = from thdl in _tongHopDuLieus
                       join tish in _timeSheets on new { NhanVienId = thdl.NhanVienId, NgayLamViec = thdl.NgayLamViec } equals new { NhanVienId = tish.NhanVienId, NgayLamViec = tish.NgayLamViec } into ps
                       from tish in ps.DefaultIfEmpty()
                       where thdl.NhanVienId == nhanvienId && thdl.NgayLamViec.Year == Nam && thdl.NgayLamViec.Month == Thang
                       orderby thdl.NgayLamViec descending
                       select new GetTongHopDuLieusByNhanVienViewModel
                       {
                           Id = thdl.Id,
                           NhanVienId = thdl.NhanVienId,
                           NgayLamViec = thdl.NgayLamViec,
                           isNgayLe = thdl.isNgayLe,
                           isCuoiTuan = thdl.isCuoiTuan,

                           Timesheet_GioVao = thdl.Timesheet_GioVao,
                           Timesheet_GioRa = thdl.Timesheet_GioRa,
                           Timesheet_NgayCong = thdl.Timesheet_NgayCong,
                           Timesheet_GioCong = thdl.Timesheet_GioCong,

                           DiTre = thdl.DiTre,
                           VeSom = thdl.VeSom,

                           NgayCong = thdl.NgayCong,
                           Final_GioCong = thdl.Final_GioCong,

                           Final_GioVao = thdl.Final_GioVao,
                           Final_GioRa = thdl.Final_GioRa,

                           TsNxd1 = tish.NguoiXetDuyetCap1Id,
                           TsNxd1TrangThai = tish.NXD1_TrangThai,
                           TsNxd1GhiChu = tish.NXD1_GhiChu,

                           TsNxd2 = tish.NguoiXetDuyetCap2Id,
                           TsNxd2TrangThai = tish.NXD2_TrangThai,
                           TsNxd2GhiChu = tish.NXD2_GhiChu,

                           TsHRxd = tish.HRXetDuyetId,
                           TsHRxdTrangThai = tish.HR_TrangThai,
                           TsHRxdGhiChu = tish.HR_GhiChu,

                           TsDieuChinhGioVao = tish.DieuChinh_GioVao,
                           TsDieuChinhGioRa = tish.DieuChinh_GioRa,
                           TsDieuChinhGhiChu = tish.DieuChinh_GhiChu,

                           TsId = tish.Id,
                           TsTrangThai = tish.TrangThai,
                           DaysToNow = (DateTime.Now - thdl.NgayLamViec).Days
                       };

            this._totalItem = ts.Count();

            return await list
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<GetTongHopDuLieusByNhanVienViewModel>> S2_GetTimesheetsInMonth(Guid nhanvienId, int Thang, int Nam)
        {
            string sqlGetDetail = string.Format("[{0}].[{1}] '{2}', {3}, {4}", Schemas.NHANSU, Procedures.SP_GetTimesheetsInMonthByNhanVien, nhanvienId, Thang, Nam);

            return _dbContext.Set<GetTongHopDuLieusByNhanVienViewModel>().FromSqlRaw<GetTongHopDuLieusByNhanVienViewModel>(sqlGetDetail).AsEnumerable().ToList();
        }

        public async Task<IReadOnlyList<GetTongHopDuLieuNhanVienVModel>> S2_GetTongHopDuLieuNhanVien(Guid nhanvienId, DateTime thoiGian)
        {
            var parameter = new[]
            {
                
                new SqlParameter("@nhanVienId",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = nhanvienId.ToString()},
                new SqlParameter("@thoiGian",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thoiGian},
            };

            string sql = string.Format("[{0}].[{1}] @nhanVienId, @thoiGian", Schemas.NHANSU, Procedures.SP_GetTongHopDuLieuNhanVien);

            try
            {
                var result = await _dbContext.Set<GetTimesheetsHrViewResults>()
                                            .FromSqlRaw(sql.ToString(), parameter)
                                            .ToListAsync();

                if (result.FirstOrDefault() != null)
                    return JsonSerializer.Deserialize<IReadOnlyList<GetTongHopDuLieuNhanVienVModel>>(result.FirstOrDefault().Result);

                return null;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<QuanLyNgayCongViewModel>> S2_QuanLyNgayCong(int pageNumber, int pageSize, DateTime thang, int phongId, string keyword, string orderBy)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@pageNumber",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageNumber},
                    new SqlParameter("@pageSize",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageSize},
                    new SqlParameter("@thang",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thang},
                    new SqlParameter("@phongId",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(phongId.ToString()) ? 0 : phongId},
                    new SqlParameter("@keyword",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(keyword) ? string.Empty : keyword},
                    new SqlParameter("@orderBy",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(orderBy) ? string.Empty : orderBy},
                    new SqlParameter("@TotalItems",SqlDbType.Int) {Direction = ParameterDirection.InputOutput, Value = 0}
                };

                string sql = string.Format("[{0}].[{1}] @pageNumber, @pageSize, @thang, @phongId, @keyword, @orderBy, @TotalItems output", Schemas.NHANSU, Procedures.SP_QuanLyNgayCong);
                var result = await _dbContext.Set<QuanLyNgayCongViewModel>()
                                            .FromSqlRaw(sql.ToString(), parameter)
                                            .ToListAsync();

                this._totalItem = Convert.ToInt32(parameter[6].Value);

                return result;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> S2_JobTongHopDuLieu(int thang, int nam)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@month",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = thang},
                    new SqlParameter("@year",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = nam},
                    new SqlParameter("@returnValue",SqlDbType.Int) {Direction = ParameterDirection.Output}
                    //new SqlParameter() { ParameterName = "ReturnValue", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output }
                };

                var result = await _dbContext.Database.ExecuteSqlRawAsync($"EXEC @returnValue = {Schemas.NHANSU}.{Procedures.SP_TongHopDuLieu} @month, @year", parameter);

                int value = (int)parameter[2].Value;

                return value;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<GetTongHopNghiViewModel>> S2_GetTongHopNghi(int thang, int nam)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@thang",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = thang},
                    new SqlParameter("@nam",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = nam}
                };

                string sql = string.Format("[{0}].[{1}] @thang, @nam", Schemas.NHANSU, Procedures.SP_GetTongHopNghi);
                var result = await _dbContext.Set<GetTongHopNghiViewModel>()
                                            .FromSqlRaw(sql.ToString(), parameter)
                                            .ToListAsync();

                this._totalItem = result.Count();

                return result;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<GetTongHopNgayCongViewModel>> S2_GetTongHopNgayCong(Guid nhanVienId, int thang, int nam)
        {
            try
            {
                var parameter = new[]
                {

                    new SqlParameter("@nhanVienId",SqlDbType.UniqueIdentifier) {Direction = ParameterDirection.Input, Value = nhanVienId},
                    new SqlParameter("@thang",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = thang},
                    new SqlParameter("@nam",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = nam},
                };

                string sql = string.Format("[{0}].[{1}] @nhanVienId, @thang, @nam", Schemas.NHANSU, Procedures.SP_ExportExcel_TongHopNgayCong);

                var result = await _dbContext.Set<GetTimesheetsHrViewResults>()
                                            .FromSqlRaw(sql.ToString(), parameter)
                                            .ToListAsync();

                if (result.FirstOrDefault() != null)
                    return JsonSerializer.Deserialize<IReadOnlyList<GetTongHopNgayCongViewModel>>(result.FirstOrDefault().Result);

                return null;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}