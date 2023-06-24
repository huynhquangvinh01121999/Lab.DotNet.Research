using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Product.Infracstructure.Entities;

namespace Product.Infracstructure.Context
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> options) : base(options)
        {

        }

        public DbSet<Offer> Offers { get; set; }
    }
}
