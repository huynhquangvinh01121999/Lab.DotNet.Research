using EsuhaiHRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EsuhaiHRM.Infrastructure.Persistence.Configurations
{
    public class TongHopDuLieuConfiguration : IEntityTypeConfiguration<TongHopDuLieu>
    {
        public void Configure(EntityTypeBuilder<TongHopDuLieu> builder)
        {
            builder.Property(x => x.NghiLe_NgayCong)
                .HasColumnType("float")
                .IsRequired(false);
        }
    }
}
