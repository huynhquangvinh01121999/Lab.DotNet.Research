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
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetById;
using EsuhaiHRM.Infrastructure.Identity.Seeds;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsHrView;
using Microsoft.Data.SqlClient;
using System.Data;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsNotHrView;
using System.Text.Json;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBan;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV2Hr;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsPhongBanV3Hr;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class TimesheetRepositoryAsync : GenericRepositoryAsync<Timesheet>, ITimesheetRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Timesheet> _timeSheet;


        private int _totalItem = 0;

        public TimesheetRepositoryAsync(ApplicationDbContext dbContext, IdentityContext idenContext, UserManager<ApplicationUser> userManager) : base(dbContext)
        {
            _dbContext = dbContext;
            _timeSheet = dbContext.Set<Timesheet>();
        }
        public async Task<Timesheet> S2_GetByGuidAsync(Guid Id)
        {
            return await _timeSheet.Where(n => n.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<GetDieuChinhTsDetailViewModel> S2_GetDieuChinhDetail(Guid Id)
        {
            //string sqlGetDetail = string.Format("[{0}].[{1}] '{2}'", Schemas.NHANSU, Procedures.SP_GetDieuChinhTsDetail, Id);

            //return _dbContext.Set<GetDieuChinhTsDetailViewModel>().FromSqlRaw<GetDieuChinhTsDetailViewModel>(sqlGetDetail).AsEnumerable().FirstOrDefault();

            var parameter = new[]
            {
                new SqlParameter("@id",SqlDbType.UniqueIdentifier) {Direction = ParameterDirection.Input, Value = Id}
            };

            string sql = string.Format("[{0}].[{1}] @id", Schemas.NHANSU, Procedures.SP_GetDieuChinhTsDetail);

            try
            {
                var result = _dbContext.Set<GetDieuChinhTsDetailViewModel>()
                                            .FromSqlRaw(sql.ToString(), parameter).AsEnumerable().FirstOrDefault();

                return result;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<IReadOnlyList<Timesheet>> S2_GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            this._totalItem = _timeSheet.Count();

            return await _timeSheet
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
        public async Task<IReadOnlyList<Timesheet>> S2_GetTimesheetsByNhanVienIdAsync(Guid nhanvienId, int pageNumber, int pageSize)
        {
            var ts = _timeSheet
                .Where(n => n.NhanVienId == nhanvienId)
                .OrderByDescending(n => n.Nam)
                .ThenByDescending(n => n.Thang);

            this._totalItem = ts.Count();

            return await ts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(n => n.NhanVien)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IReadOnlyList<Timesheet>> S2_GetTimesheetsInMonnth(Guid nhanvienId, int Thang, int Nam)
        {
            var ts = _timeSheet
                .Where(n => n.NhanVienId == nhanvienId && n.Nam == Nam && n.Thang == Thang)
                .OrderByDescending(n => n.Nam)
                .ThenByDescending(n => n.Thang)
                .ThenByDescending(n => n.NgayLamViec);

            this._totalItem = ts.Count();

            return await ts
                .Include(n => n.NhanVien)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<GetTimesheetsHrViewModel>> S2_GetTimesheetsHrView(int pageNumber, int pageSize, int phongId, int banId, string trangThai, string keyword, DateTime thoiGianBatDau, DateTime thoiGianKetThuc)
        {
            var parameter = new[]
            {
                new SqlParameter("@pageNumber",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageNumber},
                new SqlParameter("@pageSize",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageSize},
                new SqlParameter("@phongId",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(phongId.ToString()) ? 0 : phongId},
                new SqlParameter("@banId",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(banId.ToString()) ? 0 : banId},
                new SqlParameter("@trangThai",SqlDbType.VarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(trangThai) ? "all": trangThai},
                new SqlParameter("@keyword",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(keyword) ? string.Empty: keyword},
                new SqlParameter("@thoiGianBatDau",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thoiGianBatDau},
                new SqlParameter("@thoiGianKetThuc",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thoiGianKetThuc},
                new SqlParameter("@TotalItems",SqlDbType.Int) {Direction = ParameterDirection.InputOutput, Value = 0}
            };

            string sql = string.Format("[{0}].[{1}] @pageNumber, @pageSize, @phongId, @banId, @trangThai, @keyword, @thoiGianBatDau, @thoiGianKetThuc, @TotalItems output", Schemas.NHANSU, Procedures.SP_GetTimesheetsHrView);

            try
            {
                var result = await _dbContext.Set<GetTimesheetsHrViewResults>()
                                            .FromSqlRaw(sql.ToString(), parameter)
                                            .ToListAsync();

                this._totalItem = Convert.ToInt32(parameter[8].Value);

                if (result.FirstOrDefault() != null)
                    return JsonSerializer.Deserialize<IReadOnlyList<GetTimesheetsHrViewModel>>(result.FirstOrDefault().Result);

                return null;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<GetTimesheetsNotHrViewModel>> S2_GetTimesheetsNotHrView(int pageNumber, int pageSize, Guid nhanVienId, DateTime thoiGianBatDau, DateTime thoiGianKetThuc, string trangThai, string keyword)
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
                    new SqlParameter("@trangThai",SqlDbType.VarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(trangThai) ? "all": trangThai},
                    new SqlParameter("@keyword",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(keyword) ? string.Empty: keyword},
                    new SqlParameter("@TotalItems",SqlDbType.Int) {Direction = ParameterDirection.InputOutput, Value = 0}
                };

                string sql = string.Format("[{0}].[{1}] @pageNumber, @pageSize, @nhanvienId, @thoigianbatdau, @thoigianketthuc, @trangThai, @keyword, @TotalItems output", Schemas.NHANSU, Procedures.SP_GetTimesheetsNotHrView);

                var result = await _dbContext.Set<GetTimesheetsPhongBanResults>()
                                            .FromSqlRaw(sql.ToString(), parameter).ToListAsync();

                this._totalItem = Convert.ToInt32(parameter[7].Value);

                if (result.FirstOrDefault() != null)
                    return JsonSerializer.Deserialize<IReadOnlyList<GetTimesheetsNotHrViewModel>>(result.FirstOrDefault().Result);

                return null;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<GetTimesheetsPhongBanViewModel>> SP_GetTimesheetsPhongBan(int pageNumber, int pageSize, DateTime thoiGian)
        {
            var parameter = new[]
            {
                new SqlParameter("@pageNumber",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageNumber},
                new SqlParameter("@pageSize",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageSize},
                new SqlParameter("@thoiGian",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thoiGian},
                new SqlParameter("@TotalItems",SqlDbType.Int) {Direction = ParameterDirection.InputOutput, Value = 0}
            };

            string sql = string.Format("[{0}].[{1}] @pageNumber, @pageSize, @thoiGian, @TotalItems output", Schemas.NHANSU, Procedures.SP_GetTimesheetsPhongBan);

            try
            {
                var result = await _dbContext.Set<GetTimesheetsPhongBanResults>()
                                            .FromSqlRaw(sql.ToString(), parameter).ToListAsync();

                this._totalItem = Convert.ToInt32(parameter[3].Value);

                if (result.FirstOrDefault() != null)
                    return JsonSerializer.Deserialize<IReadOnlyList<GetTimesheetsPhongBanViewModel>>(result.FirstOrDefault().Result);

                return null;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<GetTimesheetsPhongBanViewModel>> SP_GetTimesheetsPhongBanHr(int pageNumber, int pageSize, DateTime thoiGian, int phongId, int banId, string keyword)
        {
            var parameter = new[]
            {
                new SqlParameter("@pageNumber",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageNumber},
                new SqlParameter("@pageSize",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageSize},
                new SqlParameter("@thoiGian",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thoiGian},
                new SqlParameter("@phongId",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(phongId.ToString()) ? 0 : phongId},
                new SqlParameter("@banId",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(banId.ToString()) ? 0 : banId},
                new SqlParameter("@keyword",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(keyword) ? string.Empty: keyword},
                new SqlParameter("@TotalItems",SqlDbType.Int) {Direction = ParameterDirection.InputOutput, Value = 0}
            };

            string sql = string.Format("[{0}].[{1}] @pageNumber, @pageSize, @thoiGian, @phongId, @banId, @keyword, @TotalItems output", Schemas.NHANSU, Procedures.SP_GetTimesheetsPhongBanHr);

            try
            {
                var result = await _dbContext.Set<GetTimesheetsPhongBanResults>()
                                            .FromSqlRaw(sql.ToString(), parameter).ToListAsync();

                this._totalItem = Convert.ToInt32(parameter[6].Value);

                if (result.FirstOrDefault() != null)
                    return JsonSerializer.Deserialize<IReadOnlyList<GetTimesheetsPhongBanViewModel>>(result.FirstOrDefault().Result);

                return null;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<GetTimesheetsPhongBanViewModel>> SP_GetTimesheetsPhongBanC1C2(int pageNumber, int pageSize, DateTime thoiGian, Guid nhanVienId, string keyword)
        {
            var parameter = new[]
            {
                new SqlParameter("@pageNumber",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageNumber},
                new SqlParameter("@pageSize",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageSize},
                new SqlParameter("@thoiGian",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thoiGian},
                new SqlParameter("@nhanvienId",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = nhanVienId.ToString()},
                new SqlParameter("@keyword",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(keyword) ? string.Empty: keyword},
                new SqlParameter("@TotalItems",SqlDbType.Int) {Direction = ParameterDirection.InputOutput, Value = 0}
            };

            string sql = string.Format("[{0}].[{1}] @pageNumber, @pageSize, @thoiGian, @nhanvienId, @keyword, @TotalItems output", Schemas.NHANSU, Procedures.SP_GetTimesheetsPhongBanC1C2);

            try
            {
                var result = await _dbContext.Set<GetTimesheetsPhongBanResults>()
                                            .FromSqlRaw(sql.ToString(), parameter).ToListAsync();

                this._totalItem = Convert.ToInt32(parameter[5].Value);

                if (result.FirstOrDefault() != null)
                    return JsonSerializer.Deserialize<IReadOnlyList<GetTimesheetsPhongBanViewModel>>(result.FirstOrDefault().Result);

                return null;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<GetTimesheetsPhongBanV2HrViewModel>> SP_GetTimesheetsPhongBanV2Hr(int pageNumber, int pageSize, DateTime thoiGian, int phongId, int banId, string keyword)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@pageNumber",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageNumber},
                    new SqlParameter("@pageSize",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageSize},
                    new SqlParameter("@thoiGian",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thoiGian},
                    new SqlParameter("@phongId",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(phongId.ToString()) ? 0 : phongId},
                    new SqlParameter("@banId",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(banId.ToString()) ? 0 : banId},
                    new SqlParameter("@keyword",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(keyword) ? string.Empty: keyword},
                    new SqlParameter("@TotalItems",SqlDbType.Int) {Direction = ParameterDirection.InputOutput, Value = 0}
                };

                string sql = string.Format("[{0}].[{1}] @pageNumber, @pageSize, @thoiGian, @phongId, @banId, @keyword, @TotalItems output", Schemas.NHANSU, Procedures.SP_GetTimesheetsPhongBanV2Hr);


                var result = await _dbContext.Set<GetTimesheetsPhongBanResults>()
                                            .FromSqlRaw(sql.ToString(), parameter).ToListAsync();

                this._totalItem = Convert.ToInt32(parameter[6].Value);

                if (result.FirstOrDefault() != null)
                    return JsonSerializer.Deserialize<IReadOnlyList<GetTimesheetsPhongBanV2HrViewModel>>(result.FirstOrDefault().Result);

                return null;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<GetTimesheetsPhongBanV2HrViewModel>> SP_GetTimesheetsPhongBanV2C1C2(int pageNumber, int pageSize, DateTime thoiGian, Guid nhanVienId, string keyword)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@pageNumber",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageNumber},
                    new SqlParameter("@pageSize",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageSize},
                    new SqlParameter("@thoiGian",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thoiGian},
                    new SqlParameter("@nhanvienId",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = nhanVienId.ToString()},
                    new SqlParameter("@keyword",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(keyword) ? string.Empty: keyword},
                    new SqlParameter("@TotalItems",SqlDbType.Int) {Direction = ParameterDirection.InputOutput, Value = 0}
                };

                string sql = string.Format("[{0}].[{1}] @pageNumber, @pageSize, @thoiGian, @nhanvienId, @keyword, @TotalItems output", Schemas.NHANSU, Procedures.SP_GetTimesheetsPhongBanV2C1C2);

                var result = await _dbContext.Set<GetTimesheetsPhongBanResults>()
                                            .FromSqlRaw(sql.ToString(), parameter).ToListAsync();

                this._totalItem = Convert.ToInt32(parameter[5].Value);

                if (result.FirstOrDefault() != null)
                    return JsonSerializer.Deserialize<IReadOnlyList<GetTimesheetsPhongBanV2HrViewModel>>(result.FirstOrDefault().Result);

                return null;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<GetTimesheetsPhongBanV3HrViewModel>> SP_GetTimesheetsPhongBanV3Hr(int pageNumber, int pageSize, DateTime thoiGian, int phongId, int banId, string keyword)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@pageNumber",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageNumber},
                    new SqlParameter("@pageSize",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageSize},
                    new SqlParameter("@thoiGian",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thoiGian},
                    new SqlParameter("@phongId",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(phongId.ToString()) ? 0 : phongId},
                    new SqlParameter("@banId",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(banId.ToString()) ? 0 : banId},
                    new SqlParameter("@keyword",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(keyword) ? string.Empty: keyword},
                    new SqlParameter("@TotalItems",SqlDbType.Int) {Direction = ParameterDirection.InputOutput, Value = 0}
                };

                string sql = string.Format("[{0}].[{1}] @pageNumber, @pageSize, @thoiGian, @phongId, @banId, @keyword, @TotalItems output", Schemas.NHANSU, Procedures.SP_GetTimesheetsPhongBanV3Hr);


                var result = await _dbContext.Set<GetTimesheetsPhongBanResults>()
                                            .FromSqlRaw(sql.ToString(), parameter).ToListAsync();

                this._totalItem = Convert.ToInt32(parameter[6].Value);

                if (result.FirstOrDefault() != null)
                    return JsonSerializer.Deserialize<IReadOnlyList<GetTimesheetsPhongBanV3HrViewModel>>(result.FirstOrDefault().Result);

                return null;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<GetTimesheetsPhongBanV3HrViewModel>> SP_GetTimesheetsPhongBanV3C1C2(int pageNumber, int pageSize, DateTime thoiGian, Guid nhanVienId, string keyword)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@pageNumber",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageNumber},
                    new SqlParameter("@pageSize",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageSize},
                    new SqlParameter("@thoiGian",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thoiGian},
                    new SqlParameter("@nhanvienId",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = nhanVienId.ToString()},
                    new SqlParameter("@keyword",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(keyword) ? string.Empty: keyword},
                    new SqlParameter("@TotalItems",SqlDbType.Int) {Direction = ParameterDirection.InputOutput, Value = 0}
                };

                string sql = string.Format("[{0}].[{1}] @pageNumber, @pageSize, @thoiGian, @nhanvienId, @keyword, @TotalItems output", Schemas.NHANSU, Procedures.SP_GetTimesheetsPhongBanV3C1C2);

                var result = await _dbContext.Set<GetTimesheetsPhongBanResults>()
                                            .FromSqlRaw(sql.ToString(), parameter).ToListAsync();

                this._totalItem = Convert.ToInt32(parameter[5].Value);

                if (result.FirstOrDefault() != null)
                    return JsonSerializer.Deserialize<IReadOnlyList<GetTimesheetsPhongBanV3HrViewModel>>(result.FirstOrDefault().Result);

                return null;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Timesheet> S2_GetTimesheetByDate(Guid nhanvienId, DateTime thoiGian)
        {
            try
            {
                var ts = await _timeSheet.Where(x => x.NhanVienId == nhanvienId
                                                        && x.NgayLamViec.Day == thoiGian.Day
                                                        && x.NgayLamViec.Month == thoiGian.Month
                                                        && x.NgayLamViec.Year == thoiGian.Year).FirstOrDefaultAsync();
                return ts;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}