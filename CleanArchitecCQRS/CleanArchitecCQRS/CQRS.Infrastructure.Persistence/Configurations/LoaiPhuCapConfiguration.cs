using EsuhaiHRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EsuhaiHRM.Infrastructure.Persistence.Configurations
{
    public class LoaiPhuCapConfiguration : IEntityTypeConfiguration<LoaiPhuCap>
    {
        public void Configure(EntityTypeBuilder<LoaiPhuCap> builder)
        {
            builder.Property(x => x.Code)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired(true);

            builder.Property(x => x.Ten)
                .HasColumnType("nvarchar(250)")
                .HasMaxLength(250)
                .IsRequired(true);

            builder.Property(x => x.MoTa)
                .HasColumnType("nvarchar(1000)")
                .HasMaxLength(1000)
                .IsRequired(true);

            builder.Property(x => x.SoTien)
                .HasColumnType("float")
                .IsRequired(false);
        }
    }
}
