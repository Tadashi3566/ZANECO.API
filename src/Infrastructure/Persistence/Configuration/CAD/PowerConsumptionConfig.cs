using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.CAD;

public class PowerConsumptionConfig : IEntityTypeConfiguration<PowerConsumption>
{
    public void Configure(EntityTypeBuilder<PowerConsumption> builder)
    {
        _ = builder.ToTable("PowerConsumptions", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.GroupId)
            .HasMaxLength(36);
        _ = builder.Property(b => b.GroupCode)
            .HasMaxLength(16);
        _ = builder.Property(b => b.GroupName)
            .HasMaxLength(32);
        _ = builder.Property(b => b.BillMonth)
            .HasMaxLength(4);
        _ = builder.Property(b => b.KWHPurchased)
            .HasColumnType("Decimal(12,2)")
                .HasDefaultValue(0);
    }
}