using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class NghiLeRepositoryAsync : GenericRepositoryAsync<NghiLe>, INghiLeRepositoryAsync
    {
        private readonly DbSet<NghiLe> _nghiLes;
        private readonly ApplicationDbContext _dbContext;

        private int _totalItem = 0;

        public NghiLeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _nghiLes = dbContext.Set<NghiLe>();
            _dbContext = dbContext;
        }

        public async Task<int> GetTotalItem()
        {
            return _totalItem;
        }

        public async Task<NghiLe> S2_GetNghiLeByIdAsync(int id)
        {
            try
            {
                var nghile = await _dbContext.NghiLes.Where(x => x.Id == id).FirstOrDefaultAsync();
                return nghile;
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<NghiLe>> S2_GetNghiLesAsync(int nam)
        {
            try
            {
                var results = await _dbContext.NghiLes.Where(x => (((DateTime)x.Ngay).Year == nam) || x.Ngay == null).ToListAsync();
                return results.OrderBy(x => x.Ngay);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> S2_isExistNghiLe(DateTime? ngay, DateTime? ngayCoDinh)
        {
            bool isExist = false;
            try
            {
                if(ngay != null)
                {
                    var ngayle = await _dbContext.NghiLes.Where(x => (((DateTime)x.Ngay).Year == ((DateTime)ngay).Year)
                                                              && (((DateTime)x.Ngay).Month == ((DateTime)ngay).Month)
                                                              && (((DateTime)x.Ngay).Day == ((DateTime)ngay).Day)).FirstOrDefaultAsync();

                    var ngaylecodinh = await _dbContext.NghiLes.Where(x => (((DateTime)x.NgayCoDinh).Month == ((DateTime)ngay).Month)
                                                                        && (((DateTime)x.NgayCoDinh).Day == ((DateTime)ngay).Day)).FirstOrDefaultAsync();
                    isExist = (ngayle != null || ngaylecodinh != null) ? true : false;
                }

                if(ngayCoDinh != null)
                {
                    var ngayle = await _dbContext.NghiLes.Where(x => (((DateTime)x.Ngay).Month == ((DateTime)ngayCoDinh).Month)
                                                                  && (((DateTime)x.Ngay).Day == ((DateTime)ngayCoDinh).Day)).FirstOrDefaultAsync();

                    var ngaylecodinh = await _dbContext.NghiLes.Where(x => (((DateTime)x.NgayCoDinh).Month == ((DateTime)ngayCoDinh).Month)
                                                                        && (((DateTime)x.NgayCoDinh).Day == ((DateTime)ngayCoDinh).Day)).FirstOrDefaultAsync();

                    isExist = (ngayle != null || ngaylecodinh != null) ? true : false;
                }
                return isExist;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
