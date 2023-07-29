using Lab.PostgreSQL.Basic.Contexts;
using Lab.PostgreSQL.Basic.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab.PostgreSQL.Basic.Repositories
{
    public class ProductRepositoryAsync : IProductRepositoryAsync
    {
        private readonly PostgreSqlDbContext _context;

        public ProductRepositoryAsync(PostgreSqlDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProductAsync(Product prod)
        {
            try
            {
                await _context.Products.AddAsync(prod);
                await _context.SaveChangesAsync();
                return prod;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteProductAsync(Product prod)
        {
            try
            {
                _context.Products.Remove(prod);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                return await _context.Products.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> UpdateProductAsync(Product prod)
        {
            try
            {
                _context.Entry(prod).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return prod;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
