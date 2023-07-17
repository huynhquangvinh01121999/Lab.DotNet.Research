using GraphQL.Types;

namespace Lab.GraphQL.Basic.GraphQL.Types
{
    public class CustomerInputType : InputObjectGraphType
    {
        public CustomerInputType()
        {
            Name = "CustomerInput";
            Field<NonNullGraphType<IntGraphType>>("id", "Customer Id");
            Field<NonNullGraphType<StringGraphType>>("firstName", "Customer FirstName");
            Field<NonNullGraphType<StringGraphType>>("lastName", "Customer LastName");
            Field<NonNullGraphType<StringGraphType>>("contact", "Customer Contact");
            Field<NonNullGraphType<StringGraphType>>("email", "Customer Email");
            Field<DateTimeGraphType>("dateOfBirth", "Customer DateOfBirth");
        }
    }
}
