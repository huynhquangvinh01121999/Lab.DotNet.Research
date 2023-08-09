using Lab.PostgreSQL.Basic.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab.PostgreSQL.Basic.Contexts
{
    public class PostgreSqlDbContext : DbContext
    {
        public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Xác định tên schema cho bảng "Products"
            modelBuilder.Entity<Product>().ToTable("products", schema: "dbo");

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
