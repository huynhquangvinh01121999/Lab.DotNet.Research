using EsuhaiHRM.Application.Features.CauHinhNgayCongs.Queries.GetCauHinhNgayCongs;
using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Identity.Seeds;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class CauHinhNgayCongRepositoryAsync : GenericRepositoryAsync<CauHinhNgayCong>, ICauHinhNgayCongRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private int _totalItem = 0;
        public CauHinhNgayCongRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetTotalItem()
        {
            return _totalItem;
        }

        public async Task<CauHinhNgayCong> S2_GetCauHinhNgayCongByDate(int? thang, int? nam)
        {
            try
            {
                var result = await _dbContext.CauHinhNgayCongs.Where(x => x.Nam == nam && x.Thang == thang).FirstOrDefaultAsync();
                return result;
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CauHinhNgayCong> S2_GetCauHinhNgayCongByIdAsync(int id)
        {
            try
            {
                var result = await _dbContext.CauHinhNgayCongs.Where(x => x.Id == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CauHinhNgayCong>> S2_GetCauHinhNgayCongsAsync(int nam)
        {
            try
            {
               var results = await _dbContext.CauHinhNgayCongs.Where(x => x.Nam == nam).ToListAsync();
               return results.OrderBy(x => x.Thang);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
