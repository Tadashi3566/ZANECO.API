using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.PayrollManager;

internal class PayrollAdjustmentConfig : IEntityTypeConfiguration<PayrollAdjustment>
{
    public void Configure(EntityTypeBuilder<PayrollAdjustment> builder)
    {
        _ = builder.ToTable("PayrollAdjustments", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.PayrollName)
            .IsRequired()
            .HasMaxLength(256);
        _ = builder.Property(b => b.AdjustmentName)
            .IsRequired()
            .HasMaxLength(256);
    }
}