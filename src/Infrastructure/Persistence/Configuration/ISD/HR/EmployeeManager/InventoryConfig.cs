using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.EmployeeManager;

internal class InventoryConfig : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.MrCode)
            .IsRequired()
            .HasMaxLength(16);

        _ = builder.Property(b => b.ItemCode)
            .IsRequired()
            .HasMaxLength(16);

        _ = builder.Property(b => b.DateReceived)
            .HasColumnType("datetime(6)");

        _ = builder.Property(b => b.Cost)
            .HasColumnType("decimal(12,2)");
    }
}