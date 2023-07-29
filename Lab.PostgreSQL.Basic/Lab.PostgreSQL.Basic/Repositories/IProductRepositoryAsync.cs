using Lab.PostgreSQL.Basic.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab.PostgreSQL.Basic.Repositories
{
    public interface IProductRepositoryAsync
    {
        Task<Product> AddProductAsync(Product prod);
        Task<Product> UpdateProductAsync(Product prod);
        Task DeleteProductAsync(Product prod);
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetProducts();
    }
}
