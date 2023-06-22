using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetDetailViecBenNgoai;
using Microsoft.Data.SqlClient;
using System.Data;
using EsuhaiHRM.Infrastructure.Identity.Seeds;
using System.Collections.Generic;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetViecBenNgoaisNotHrView;
using EsuhaiHRM.Application.Features.ViecBenNgoais.Queries.GetViecBenNgoaiHrView;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class ViecBenNgoaiRepositoryAsync : GenericRepositoryAsync<ViecBenNgoai>, IViecBenNgoaiRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private int _totalItem = 0;

        public ViecBenNgoaiRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetTotalItem()
        {
            return _totalItem;
        }

        public async Task<ViecBenNgoai> S2_GetByGuidAsync(Guid Id)
        {
            return await _dbContext.Set<ViecBenNgoai>().Where(n => n.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<GetDetailViecBenNgoaiViewModel> S2_GetDetailByGuidAsync(Guid Id, Guid nhanVienId)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@id",SqlDbType.UniqueIdentifier) {Direction = ParameterDirection.Input, Value = Id},
                    new SqlParameter("@nhanVienId",SqlDbType.UniqueIdentifier) {Direction = ParameterDirection.Input, Value = nhanVienId}
                };

                string sql = string.Format("[{0}].[{1}] @id, @nhanVienId", Schemas.NHANSU, Procedures.SP_GetViecBenNgoaiDetail);

                var result = _dbContext.Set<GetDetailViecBenNgoaiViewModel>().FromSqlRaw(sql.ToString(), parameter).AsEnumerable().FirstOrDefault();

                return result;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<GetViecBenNgoaiHrViewModel>> S2_GetViecBenNgoaisHrView(int pageNumber, int pageSize, int phongId, int banId, string trangThai, string keyword, DateTime thoiGianBatDau, DateTime thoiGianKetThuc)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@pageNumber",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageNumber},
                    new SqlParameter("@pageSize",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = pageSize},
                    new SqlParameter("@phongId",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(phongId.ToString()) ? 0 : phongId},
                    new SqlParameter("@banId",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(banId.ToString()) ? 0 : banId},
                    new SqlParameter("@trangThai",SqlDbType.VarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(trangThai) ? "all" : trangThai},
                    new SqlParameter("@keyword",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(keyword) ? string.Empty: keyword},
                    new SqlParameter("@thoiGianBatDau",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thoiGianBatDau},
                    new SqlParameter("@thoiGianKetThuc",SqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = thoiGianKetThuc},
                    new SqlParameter("@TotalItems",SqlDbType.Int) {Direction = ParameterDirection.InputOutput, Value = 0}
                };

                string sql = string.Format("[{0}].[{1}] @pageNumber, @pageSize, @phongId, @banId, @trangThai, @keyword, @thoiGianBatDau, @thoiGianKetThuc, @TotalItems output", Schemas.NHANSU, Procedures.SP_GetViecBenNgoaisHrView);

                var viecBenNgoais = await _dbContext.Set<GetViecBenNgoaiHrViewModel>()
                                            .FromSqlRaw(sql.ToString(), parameter)
                                            .ToListAsync();

                this._totalItem = Convert.ToInt32(parameter[8].Value);

                return viecBenNgoais;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<GetViecBenNgoaisNotHrViewModel>> S2_GetViecBenNgoaisNotHrView(int pageNumber, int pageSize, Guid nhanVienId, DateTime thoiGianBatDau, DateTime thoiGianKetThuc, string trangThai, string keyword)
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
                    new SqlParameter("@trangThai",SqlDbType.VarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(trangThai) ? "all" : trangThai},
                    new SqlParameter("@keyword",SqlDbType.NVarChar) {Direction = ParameterDirection.Input, Value = string.IsNullOrEmpty(keyword) ? string.Empty: keyword},
                    new SqlParameter("@TotalItems",SqlDbType.Int) {Direction = ParameterDirection.InputOutput, Value = 0}
                };

                string sql = string.Format("[{0}].[{1}] @pageNumber, @pageSize, @nhanvienId, @thoigianbatdau, @thoigianketthuc, @trangThai, @keyword, @TotalItems output", Schemas.NHANSU, Procedures.SP_GetViecBenNgoaisNotHrView);

                var viecBenNgoais = await _dbContext.Set<GetViecBenNgoaisNotHrViewModel>()
                                                .FromSqlRaw(sql.ToString(), parameter)
                                                .ToListAsync();

                this._totalItem = Convert.ToInt32(parameter[7].Value);

                return viecBenNgoais;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
