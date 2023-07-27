using Lab.PostgreSQL.Basic.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab.PostgreSQL.Basic.Repositories
{
    public interface IProductRepositoryAsync
    {
        Task<Product> AddProductAsync(Product patient);
        Task<Product> UpdateProductAsync(Product patient);
        Task DeleteProductAsync(int id);
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetProducts();
    }
}
