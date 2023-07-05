using Lab.GraphQL.Basic.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab.GraphQL.Basic.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
