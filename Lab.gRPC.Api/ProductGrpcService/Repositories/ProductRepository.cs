using Microsoft.EntityFrameworkCore;
using Product.Infracstructure.Context;
using Product.Infracstructure.Entities;
using Product.Infracstructure.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductGrpcService.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbContextClass _dbContext;

        public ProductRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Offer>> GetListAsync()
        {
            return await _dbContext.Offers.ToListAsync();
        }

        public async Task<Offer> GetByIdAsync(int Id)
        {
            return await _dbContext.Offers.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Offer> AddAsync(Offer offer)
        {
            var result = _dbContext.Offers.Add(offer);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Offer> UpdateAsync(Offer offer)
        {
            var result = _dbContext.Offers.Update(offer);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<bool> DeleteAsync(int Id)
        {
            var filteredData = _dbContext.Offers.Where(x => x.Id == Id).FirstOrDefault();
            var result = _dbContext.Remove(filteredData);
            await _dbContext.SaveChangesAsync();
            return result != null ? true : false;
        }
    }
}
