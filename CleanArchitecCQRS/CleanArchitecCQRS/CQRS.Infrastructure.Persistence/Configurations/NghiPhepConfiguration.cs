using EsuhaiHRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EsuhaiHRM.Infrastructure.Persistence.Configurations
{
    public class NghiPhepConfiguration : IEntityTypeConfiguration<NghiPhep>
    {
        public void Configure(EntityTypeBuilder<NghiPhep> builder)
        {
            builder.Property(x => x.ThoiGianBatDau)
                .HasColumnType("datetime")
                .IsRequired(true);

            builder.Property(x => x.ThoiGianKetThuc)
                .HasColumnType("datetime")
                .IsRequired(true);

            builder.Property(x => x.SoNgayDangKy)
                .HasColumnType("float")
                .IsRequired(false);

            builder.Property(x => x.TrangThaiDangKy)
                .HasColumnType("nvarchar(30)")
                .IsRequired(false);

            builder.Property(x => x.TrangThaiNghi)
                .HasColumnType("nvarchar(30)")
                .IsRequired(false);

            builder.Property(x => x.MoTa)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);
        }
    }
}
