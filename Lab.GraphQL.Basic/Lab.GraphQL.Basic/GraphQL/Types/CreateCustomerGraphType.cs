using GraphQL.Types;

namespace Lab.GraphQL.Basic.GraphQL.Types
{
    public class CreateCustomerGraphType : InputObjectGraphType
    {
        public CreateCustomerGraphType()
        {
            Name = "CreateCustomer";
            Field<NonNullGraphType<IntGraphType>>("id", "Customer Id");
            Field<NonNullGraphType<StringGraphType>>("firstName", "Customer FirstName");
            Field<NonNullGraphType<StringGraphType>>("lastName", "Customer LastName");
            Field<NonNullGraphType<StringGraphType>>("contact", "Customer Contact");
            Field<NonNullGraphType<StringGraphType>>("email", "Customer Email");
            //Field<DateTimeGraphType>("dateOfBirth", "Customer DateOfBirth");
            Field<DateTimeGraphType>("dateOfBirth", expression: null, nullable : true, type: typeof(DateTimeGraphType));

            //Field(x => x.Id).Description("Customer Id");
            //Field(x => x.Id).Description("Customer Id").Name("id");
            //    Field(x => x.FirstName).Description("Customer's First Name");
            //    Field(x => x.LastName).Description("Customer's Last Name");
            //    Field(x => x.Contact).Description("Customer's Contact");
            //    Field(x => x.Email).Description("Customer's Email");
            //    Field(x => x.DateOfBirth).Description("Customer's DoB");
        }
    }
}
