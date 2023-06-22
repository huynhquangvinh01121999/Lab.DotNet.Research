using Microsoft.EntityFrameworkCore;
using Product.Infracstructure.Context;
using Product.Infracstructure.Entities;
using Product.Infracstructure.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductGrpcService.Repositories
{
    public class ProductOfferRepository : IProductOfferRepository
    {
        private readonly DbContextClass _dbContext;

        public ProductOfferRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Offer>> GetOfferListAsync()
        {
            return await _dbContext.Offers.ToListAsync();
        }

        public async Task<Offer> GetOfferByIdAsync(int Id)
        {
            return await _dbContext.Offers.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Offer> AddOfferAsync(Offer offer)
        {
            var result = _dbContext.Offers.Add(offer);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Offer> UpdateOfferAsync(Offer offer)
        {
            var result = _dbContext.Offers.Update(offer);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<bool> DeleteOfferAsync(int Id)
        {
            var filteredData = _dbContext.Offers.Where(x => x.Id == Id).FirstOrDefault();
            var result = _dbContext.Remove(filteredData);
            await _dbContext.SaveChangesAsync();
            return result != null ? true : false;
        }
    }
}
