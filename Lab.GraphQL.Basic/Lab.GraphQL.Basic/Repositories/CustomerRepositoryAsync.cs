using Lab.GraphQL.Basic.Context;
using Lab.GraphQL.Basic.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.GraphQL.Basic.Repositories
{
    public class CustomerRepositoryAsync : ICustomerRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(Customer entity)
        {
            await _dbContext.Customers.AddAsync(entity);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetDetail(int id)
        {
            return await _dbContext.Customers.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
