using Lab.GraphQL.Basic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab.GraphQL.Basic.Repositories
{
    public interface ICustomerRepositoryAsync
    {
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetDetail(int id);
        Task<int> CreateAsync(Customer entity);
    }
}
