using EsuhaiHRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EsuhaiHRM.Infrastructure.Persistence.Configurations
{
    public class ViecBenNgoaiConfiguration : IEntityTypeConfiguration<ViecBenNgoai>
    {
        public void Configure(EntityTypeBuilder<ViecBenNgoai> builder)
        {
            builder.Property(x => x.ThoiGianBatDau)
                .HasColumnType("datetime")
                .IsRequired(true);

            builder.Property(x => x.ThoiGianKetThuc)
                .HasColumnType("datetime")
                .IsRequired(true);

            builder.Property(x => x.LoaiCongTac)
                .HasColumnType("nvarchar(20)")
                .IsRequired(false);

            builder.Property(x => x.TrangThaiXetDuyet)
                .HasColumnType("nvarchar(30)")
                .IsRequired(false);

            builder.Property(x => x.MoTa)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            builder.Property(x => x.DiaDiem)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);

            builder.Property(x => x.SoGio)
                .HasColumnType("float")
                .IsRequired(false);
        }
    }
}
