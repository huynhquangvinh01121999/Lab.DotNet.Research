using EsuhaiHRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EsuhaiHRM.Infrastructure.Persistence.Configurations
{
    public class NghiLeConfiguration : IEntityTypeConfiguration<NghiLe>
    {
        public void Configure(EntityTypeBuilder<NghiLe> builder)
        {
            builder.Property(x => x.Ngay)
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.Property(x => x.NgayCoDinh)
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.Property(x => x.MoTa)
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);
        }
    }
}
