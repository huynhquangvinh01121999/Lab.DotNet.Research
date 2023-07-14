using GraphQL;
using GraphQL.Types;
using Lab.GraphQL.Basic.Context;
using Lab.GraphQL.Basic.GraphQL.Types;
using Lab.GraphQL.Basic.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.GraphQL.Basic.GraphQL.Queries
{
    public class CustomerQuery : ObjectGraphType
    {
        private readonly ApplicationDbContext _appContext;
        public CustomerQuery(ApplicationDbContext appContext)
        {
            this._appContext = appContext;
            Name = "Query";
            //Field<ListGraphType<CustomerGraphType>>("customers", "Returns a list of Customer", resolve: context => _appContext.Customers.ToList());
            //Field<CustomerGraphType>("customer", "Returns a Single Customer",
            //    new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Customer Id" }),
            //        context => _appContext.Customers.Single(x => x.Id == context.Arguments["id"].GetPropertyValue<int>()));

            Field<ListGraphType<CustomerGraphType>>("customers", "Returns a list of Customer", resolve: context => GetCustomers());
            Field<CustomerGraphType>("customer", "Returns a Single Customer",
                new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Customer Id" }),
                    context => GetDetail(context.Arguments["id"].GetPropertyValue<int>()));
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _appContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetDetail(int id)
        {
            return await _appContext.Customers.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
