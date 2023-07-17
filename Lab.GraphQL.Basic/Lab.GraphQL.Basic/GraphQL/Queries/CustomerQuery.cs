using GraphQL;
using GraphQL.Types;
using Lab.GraphQL.Basic.GraphQL.Types;
using Lab.GraphQL.Basic.Repositories;

namespace Lab.GraphQL.Basic.GraphQL.Queries
{
    public class CustomerQuery : ObjectGraphType
    {
        private readonly ICustomerRepositoryAsync _customerRepositoryAsync;
        public CustomerQuery(ICustomerRepositoryAsync customerRepositoryAsync)
        {
            _customerRepositoryAsync = customerRepositoryAsync;

            Name = "Query";
            //Field<ListGraphType<CustomerGraphType>>("customers", "Returns a list of Customer", resolve: context => _appContext.Customers.ToList());
            //Field<CustomerGraphType>("customer", "Returns a Single Customer",
            //    new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Customer Id" }),
            //        context => _appContext.Customers.Single(x => x.Id == context.Arguments["id"].GetPropertyValue<int>()));

            // Get List
            Field<ListGraphType<CustomerGraphType>>("customers", "Returns a list of Customer", resolve: context => _customerRepositoryAsync.GetCustomers());

            // Get Detail
            Field<CustomerGraphType>("customer", "Returns a Single Customer",
                new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Customer Id" }),
                    context => _customerRepositoryAsync.GetDetail(context.Arguments["id"].GetPropertyValue<int>()));
        }
    }
}
