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
using Microsoft.Data.SqlClient;
using System.Data;
using EsuhaiHRM.Infrastructure.Identity.Seeds;
using EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasNotHrView;
using EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasHrView;
using EsuhaiHRM.Application.Features.Timesheets.Queries.GetTimesheetsHrView;
using System.Text.Json;
using EsuhaiHRM.Application.Features.TangCas.Queries.GetTangCasByNhanVien;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class TangCaRepositoryAsync : GenericRepositoryAsync<TangCa>, ITangCaRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TangCa> _tangCa;


        private int _totalItem = 0;

        public TangCaRepositoryAsync(ApplicationDbContext dbContext, IdentityContext idenContext, UserManager<ApplicationUser> userManager) : base(dbContext)
        {
            _dbContext = dbContext;
            _tangCa = dbContext.Set<TangCa>();
        }

        public async Task<TangCa> S2_GetByGuidAsync(Guid Id)
        {
            return await _tangCa.Where(n => n.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<TangCa>> S2_GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            this._totalItem = _tangCa.Count();

            return await _tangCa
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

        public async Task<IReadOnlyList<GetTangCasByNhanVienViewModel>> S2_GetTangCaByDate(Guid nhanvienId, DateTime ngay)
        {
            var parameter = new[]
            {
                new SqlParameter("@ngayLamViec",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = ngay},
                new SqlParameter("@nhanVienId",SqlDbType.UniqueIdentifier) {Direction = ParameterDirection.Input, Value = nhanvienId}
            };

            string sql = string.Format("[{0}].[{1}] @ngayLamViec, @nhanVienId", Schemas.NHANSU, Procedures.SP_GetTangCasByNhanVien);

            try
            {
                var tangcas = await _dbContext.Set<GetTangCasByNhanVienViewModel>()
                                            .FromSqlRaw(sql.ToString(), parameter)
                                            .ToListAsync();

                this._totalItem = tangcas.Count();

                return tangcas;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<GetTangCasNotHrViewModel>> S2_GetTangCasNotViewHr(int pageNumber, int pageSize, Guid nhanVienId, DateTime thoiGianBatDau, DateTime thoiGianKetThuc, string trangThai, string keyword)
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

                string sql = string.Format("[{0}].[{1}] @pageNumber, @pageSize, @nhanvienId, @thoigianbatdau, @thoigianketthuc, @trangThai, @keyword, @TotalItems output", Schemas.NHANSU, Procedures.SP_GetTangCasNotHrView);

                var result = await _dbContext.Set<GetTimesheetsHrViewResults>()
                                            .FromSqlRaw(sql.ToString(), parameter)
                                            .ToListAsync();

                this._totalItem = Convert.ToInt32(parameter[7].Value);

                if (result.FirstOrDefault() != null)
                    return JsonSerializer.Deserialize<IReadOnlyList<GetTangCasNotHrViewModel>>(result.FirstOrDefault().Result);

                return null;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<GetTangCasHrViewModel>> S2_GetTangCasViewHr(int pageNumber, int pageSize, int phongId, int banId, string trangThai, string keyword, DateTime thoiGianBatDau, DateTime thoiGianKetThuc)
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

                string sql = string.Format("[{0}].[{1}] @pageNumber, @pageSize, @phongId, @banId, @trangThai, @keyword, @thoiGianBatDau, @thoiGianKetThuc, @TotalItems output", Schemas.NHANSU, Procedures.SP_GetTangCasHrView);

                //var tangcas = await _dbContext.Set<GetTangCasHrViewModel>()
                //                            .FromSqlRaw(sql.ToString(), parameter)
                //                            .ToListAsync();

                //this._totalItem = Convert.ToInt32(parameter[8].Value);

                //return tangcas;

                var result = await _dbContext.Set<GetTimesheetsHrViewResults>()
                                            .FromSqlRaw(sql.ToString(), parameter)
                                            .ToListAsync();

                this._totalItem = Convert.ToInt32(parameter[8].Value);

                if (result.FirstOrDefault() != null)
                    return JsonSerializer.Deserialize<IReadOnlyList<GetTangCasHrViewModel>>(result.FirstOrDefault().Result);

                return null;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}