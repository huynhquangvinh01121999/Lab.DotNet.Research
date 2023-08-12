using Microsoft.EntityFrameworkCore;
using WebAssemblyServer.Entities;

namespace WebAssemblyServer.Contexts
{
    public class AppDbContext : DbContext
    {
        private string defaultSchema = "Lab";
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(defaultSchema);

            builder.Entity<TodosList>().HasKey(x => x.Id);

            base.OnModelCreating(builder);
        }
        public DbSet<TodosList> TodoLists { get; set; }
    }
}
