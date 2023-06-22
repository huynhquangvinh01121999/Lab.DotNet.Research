using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using EsuhaiHRM.Application.Wrappers;
using EsuhaiHRM.Infrastructure.Identity.Seeds;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class DashBoardRepositoryAsync : GenericRepositoryAsync<DashBoard>, IDashBoardRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public DashBoardRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        [Obsolete]
        public async Task<DashBoard> S2_GetDashBoardAsync()
        {
            //Get count 
            string sqlCount = string.Format("[{0}].[{1}]",Schemas.NHANSU,Procedures.SP_GetDashBoard);
            var resultCount = _dbContext.Set<DashBoard>().FromSqlRaw(sqlCount).AsEnumerable().FirstOrDefault();

            //Get sum Hoc Van
            string sqlHocVan = string.Format("[{0}].[{1}]", Schemas.NHANSU, Procedures.SP_GetTrinhDoHocVan);
            var resultHocVan = _dbContext.Set<DashBoard_DanhMuc>().FromSqlRaw(sqlHocVan).AsEnumerable();

            //Get sum Tieng nhat
            string sqlTiengNhat = string.Format("[{0}].[{1}]", Schemas.NHANSU, Procedures.SP_GetTrinhDoTiengNhat);
            var resultTiengNhat = _dbContext.Set<DashBoard_DanhMuc>().FromSqlRaw(sqlTiengNhat).AsEnumerable();

            //Get sum Quoc tich
            string sqlQuocTich = string.Format("[{0}].[{1}]", Schemas.NHANSU, Procedures.SP_GetNhanVienQuocTich);
            var resultQuocTich = _dbContext.Set<DashBoard_DanhMuc>().FromSqlRaw(sqlQuocTich).AsEnumerable();

            //Set data to response result
            resultCount.TrinhDoHocVans = resultHocVan.ToList();
            resultCount.TrinhDoTiengNhats = resultTiengNhat.ToList();
            resultCount.QuocTichs = resultQuocTich.ToList();

            return await Task.FromResult(resultCount);
        }

        [Obsolete]
        public async Task<IEnumerable<DashBoard_12Months>> S2_GetListNhanVienInMonths(int? Year)
        {
            //Get List Months for NhanVien
            string sqlGetList = string.Format("[{0}].[{1}] @YEAR = {2}", Schemas.NHANSU, Procedures.SP_ListNhanVienMonths, Year);
            var result12Months = _dbContext.Set<DashBoard_12Months>().FromSqlRaw(sqlGetList).AsEnumerable();

            //Set data to response result
            return await Task.FromResult(result12Months);
        }
    }
}
