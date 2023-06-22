using Product.Infracstructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Infracstructure.IRepositories
{
    public interface IProductOfferRepository
    {
        public Task<List<Offer>> GetOfferListAsync();
        public Task<Offer> GetOfferByIdAsync(int Id);
        public Task<Offer> AddOfferAsync(Offer offer);
        public Task<Offer> UpdateOfferAsync(Offer offer);
        public Task<bool> DeleteOfferAsync(int Id);
    }
}
