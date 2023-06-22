using EsuhaiHRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EsuhaiHRM.Infrastructure.Persistence.Configurations
{
    public class PhuCapConfiguration : IEntityTypeConfiguration<PhuCap>
    {
        public void Configure(EntityTypeBuilder<PhuCap> builder)
        {
            builder.Property(x => x.ThoiGianBatDau)
                .HasColumnType("datetime")
                .IsRequired(true);

            builder.Property(x => x.ThoiGianKetThuc)
                .HasColumnType("datetime")
                .IsRequired(true);

            builder.Property(x => x.SoLanPhuCap)
                .HasColumnType("smallint")
                .IsRequired(false);

            builder.Property(x => x.SoBuoiSang)
                .HasColumnType("smallint")
                .IsRequired(false);

            builder.Property(x => x.SoBuoiChieu)
                .HasColumnType("smallint")
                .IsRequired(false);

            builder.Property(x => x.SoBuoiTrua)
                .HasColumnType("smallint")
                .IsRequired(false);

            builder.Property(x => x.SoQuaDem)
                .HasColumnType("smallint")
                .IsRequired(false);

            builder.Property(x => x.XD_ThoiGianBatDau)
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.Property(x => x.XD_ThoiGianKetThuc)
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.Property(x => x.XD_SoLanPhuCap)
                .HasColumnType("smallint")
                .IsRequired(false);

            builder.Property(x => x.XD_SoBuoiSang)
                .HasColumnType("smallint")
                .IsRequired(false);

            builder.Property(x => x.XD_SoBuoiChieu)
                .HasColumnType("smallint")
                .IsRequired(false);

            builder.Property(x => x.XD_SoBuoiTrua)
                .HasColumnType("smallint")
                .IsRequired(false);

            builder.Property(x => x.XD_SoQuaDem)
                .HasColumnType("smallint")
                .IsRequired(false);

            builder.Property(x => x.SoLanCuoiTuan)
                .HasColumnType("smallint")
                .IsRequired(false);

            builder.Property(x => x.SoLanNgayLe)
                .HasColumnType("smallint")
                .IsRequired(false);

            builder.Property(x => x.SoLanNgayThuong)
                .HasColumnType("smallint")
                .IsRequired(false);

            builder.Property(x => x.MoTa)
                .HasColumnType("nvarchar(1000)")
                .HasMaxLength(1000)
                .IsRequired(true);

            builder.Property(x => x.TrangThai)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.NXD1_TrangThai)
                .HasColumnType("nvarchar(30)")
                .HasMaxLength(30)
                .IsRequired(false);

            builder.Property(x => x.NXD1_GhiChu)
                .HasColumnType("nvarchar(500)")
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.NXD2_TrangThai)
                .HasColumnType("nvarchar(30)")
                .HasMaxLength(30)
                .IsRequired(false);

            builder.Property(x => x.NXD2_GhiChu)
                .HasColumnType("nvarchar(500)")
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.HR_TrangThai)
                .HasColumnType("nvarchar(30)")
                .HasMaxLength(30)
                .IsRequired(false);

            builder.Property(x => x.HR_GhiChu)
                .HasColumnType("nvarchar(500)")
                .HasMaxLength(500)
                .IsRequired(false);
        }
    }
}
