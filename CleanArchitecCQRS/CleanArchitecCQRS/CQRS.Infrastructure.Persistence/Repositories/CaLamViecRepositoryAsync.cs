using EsuhaiHRM.Application.Interfaces.Repositories;
using EsuhaiHRM.Domain.Entities;
using EsuhaiHRM.Infrastructure.Persistence.Contexts;
using EsuhaiHRM.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EsuhaiHRM.Infrastructure.Persistence.Repositories
{
    public class CaLamViecRepositoryAsync : GenericRepositoryAsync<CaLamViec>, ICaLamViecRepositoryAsync
    {
        private readonly DbSet<CaLamViec> _caLamViecs;
        private readonly DbSet<NhanVien_CaLamViec> _nhanVien_CaLamViecs;

        public CaLamViecRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _caLamViecs = dbContext.Set<CaLamViec>();
            _nhanVien_CaLamViecs = dbContext.Set<NhanVien_CaLamViec>();
        }

        public async Task<CaLamViec> S2_GetByIdAsync(int id)
        {
            return await _caLamViecs.Where(n => n.Deleted != true)
                                    .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<CaLamViec> S2_GetCaLamViecByNhanVienId(Guid nhanVienId)
        {
            try
            {
                var calamviec = from calv in _caLamViecs
                                join nv_calv in _nhanVien_CaLamViecs on calv.Id equals nv_calv.CaLamViecId
                                where nv_calv.NhanVienId == nhanVienId
                                select calv;

                return await calamviec.AsNoTracking()
                                    .FirstOrDefaultAsync();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IReadOnlyList<CaLamViec>> S2_GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _caLamViecs.Where(n => n.Deleted != true)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .AsNoTracking()
                                    .ToListAsync();
        }
    }
}
