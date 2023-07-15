using GraphQL.Types;
using Lab.GraphQL.Basic.Models;
using Lab.GraphQL.Basic.Repositories;
using System.Threading.Tasks;

namespace Lab.GraphQL.Basic.GraphQL.Mutations
{
    public class CustomerMutation : ObjectGraphType
    {
        private readonly ICustomerRepositoryAsync _customerRepositoryAsync;

        public CustomerMutation(ICustomerRepositoryAsync customerRepositoryAsync)
        {
            _customerRepositoryAsync = customerRepositoryAsync;
        }

        public async Task<int> AddItemAsync(Customer input)
        {
            return await _customerRepositoryAsync.CreateAsync(input);
        }
    }
}
