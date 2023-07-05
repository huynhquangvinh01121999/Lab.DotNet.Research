using GraphQL;
using GraphQL.Types;
using Lab.GraphQL.Basic.Context;
using Lab.GraphQL.Basic.GraphQL.Types;
using System.Linq;

namespace Lab.GraphQL.Basic.GraphQL.Queries
{
    public class CustomerQuery : ObjectGraphType
    {
        private readonly ApplicationDbContext _appContext;
        public CustomerQuery(ApplicationDbContext appContext)
        {
            this._appContext = appContext;
            Name = "Query";
            Field<ListGraphType<CustomerGraphType>>("customers", "Returns a list of Customer", resolve: context => _appContext.Customers.ToList());
            Field<CustomerGraphType>("customer", "Returns a Single Customer",
                new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Customer Id" }),
                    context => _appContext.Customers.Single(x => x.Id == context.Arguments["id"].GetPropertyValue<int>()));
        }
    }
}
