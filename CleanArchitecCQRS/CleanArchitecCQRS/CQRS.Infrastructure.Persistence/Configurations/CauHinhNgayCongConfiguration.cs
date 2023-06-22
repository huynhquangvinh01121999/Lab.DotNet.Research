using EsuhaiHRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EsuhaiHRM.Infrastructure.Persistence.Configurations
{
    public class CauHinhNgayCongConfiguration : IEntityTypeConfiguration<CauHinhNgayCong>
    {
        public void Configure(EntityTypeBuilder<CauHinhNgayCong> builder)
        {
            builder.Property(x => x.Thang)
                .HasColumnType("smallint")
                .IsRequired(false);

            builder.Property(x => x.Nam)
                .HasColumnType("smallint")
                .IsRequired(false);
        }
    }
}
