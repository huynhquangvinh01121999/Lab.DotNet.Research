using GraphQL;
using GraphQL.Types;
using Lab.GraphQL.Basic.GraphQL.Queries;

namespace Lab.GraphQL.Basic.GraphQL.Schemas
{
    public class DemoSchema : Schema
    {
        public DemoSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<CustomerQuery>();
        }
    }
}
