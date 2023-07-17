using GraphQL;
using GraphQL.Types;
using Lab.GraphQL.Basic.GraphQL.Types;
using Lab.GraphQL.Basic.Models;
using Lab.GraphQL.Basic.Repositories;

namespace Lab.GraphQL.Basic.GraphQL.Mutations
{
    public class CustomerMutation : ObjectGraphType
    {
        private readonly ICustomerRepositoryAsync _customerRepositoryAsync;
        public CustomerMutation(ICustomerRepositoryAsync customerRepositoryAsync)
        {
            _customerRepositoryAsync = customerRepositoryAsync;

            Field<IntGraphType>("CreateCustomer", "Create a new customer",
                new QueryArguments(new QueryArgument<NonNullGraphType<CustomerInputType>> { Name = "customer", Description = "Item customer created" }),
                    context => _customerRepositoryAsync.CreateAsync(context.Arguments["customer"].GetPropertyValue<Customer>()));

        }
    }
}
