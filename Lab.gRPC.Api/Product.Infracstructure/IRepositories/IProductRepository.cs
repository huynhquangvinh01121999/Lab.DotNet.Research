using Product.Infracstructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Infracstructure.IRepositories
{
    public interface IProductRepository
    {
        public Task<List<Offer>> GetListAsync();
        public Task<Offer> GetByIdAsync(int Id);
        public Task<Offer> AddAsync(Offer offer);
        public Task<Offer> UpdateAsync(Offer offer);
        public Task<bool> DeleteAsync(int Id);
    }
}
