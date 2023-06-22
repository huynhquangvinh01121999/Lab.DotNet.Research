using EsuhaiSchedule.Application.IRepositories;
using EsuhaiSchedule.Application.Models;
using EsuhaiSchedule.Persistence.Context;
using EsuhaiSchedule.Persistence.Seeds;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EsuhaiSchedule.Persistence.Repositories
{
    public class TongHopDuLieuRepositoryAsync : ITongHopDuLieuRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public TongHopDuLieuRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<GetTongHopXetDuyetViewModel>> S2_Job_GetTongHopXetDuyetC1(int nam, int thang)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@nam",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = nam},
                    new SqlParameter("@thang",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = thang}
                };

                string sql = string.Format("[{0}].[{1}] @nam, @thang", Schemas.NHANSU, Procedures.SP_Job_GetTongHopXetDuyetC1);
                var result = await _dbContext.Set<GetTongHopXetDuyetViewModel>()
                                            .FromSqlRaw(sql.ToString(), parameter)
                                            .ToListAsync();

                return result;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IReadOnlyList<GetTongHopXetDuyetViewModel>> S2_Job_GetTongHopXetDuyetC2(int nam, int thang)
        {
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@nam",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = nam},
                    new SqlParameter("@thang",SqlDbType.Int) {Direction = ParameterDirection.Input, Value = thang}
                };

                string sql = string.Format("[{0}].[{1}] @nam, @thang", Schemas.NHANSU, Procedures.SP_Job_GetTongHopXetDuyetC2);
                var result = await _dbContext.Set<GetTongHopXetDuyetViewModel>()
                                            .FromSqlRaw(sql.ToString(), parameter)
                                            .ToListAsync();

                return result;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
