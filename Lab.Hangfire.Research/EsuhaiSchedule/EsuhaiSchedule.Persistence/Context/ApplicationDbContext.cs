using EsuhaiSchedule.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace EsuhaiSchedule.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<GetTongHopXetDuyetViewModel>();
            builder.Entity<GetTongHopXetDuyetViewModel>().HasNoKey().ToView(null);
        }
    }
}
