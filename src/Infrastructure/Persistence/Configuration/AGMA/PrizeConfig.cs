using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.AGMA;

public class PrizeConfig : IEntityTypeConfiguration<Prize>
{
    public void Configure(EntityTypeBuilder<Prize> builder)
    {
        //_ = builder.ToTable("Prizes", SchemaNames.ZANECO)
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.Name)
            .IsRequired();

        _ = builder.Property(b => b.RaffleName)
            .IsRequired()
            .HasDefaultValue(default)
            .HasMaxLength(128);

        _ = builder.Property(b => b.PrizeType)
            .IsRequired()
            .HasDefaultValue(default)
            .HasMaxLength(16);

        _ = builder.Property(b => b.Winners)
            .HasDefaultValue(0);
    }
}