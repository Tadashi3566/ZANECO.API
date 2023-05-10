using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.PayrollManager;

internal class AdjustmentConfig : IEntityTypeConfiguration<Adjustment>
{
    public void Configure(EntityTypeBuilder<Adjustment> builder)
    {
        _ = builder.ToTable("Adjustments", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.AdjustmentType)
            .IsRequired()
            .HasMaxLength(16);
        _ = builder.Property(b => b.EmployeeType)
            .IsRequired()
            .HasMaxLength(16);
        _ = builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(256);
        _ = builder.Property(b => b.Amount)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnType("Decimal(12,2)");
    }
}