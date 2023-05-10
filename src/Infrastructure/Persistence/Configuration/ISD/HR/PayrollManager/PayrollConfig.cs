using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.PayrollManager;

internal class PayrollConfig : IEntityTypeConfiguration<Payroll>
{
    public void Configure(EntityTypeBuilder<Payroll> builder)
    {
        _ = builder.ToTable("Payrolls", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(256);
        _ = builder.Property(b => b.StartDate)
            .IsRequired()
            .HasColumnType("date");
        _ = builder.Property(b => b.EndDate)
            .IsRequired()
            .HasColumnType("date");
        _ = builder.Property(b => b.PayrollDate)
            .IsRequired()
            .HasColumnType("date");
        _ = builder.Property(b => b.TotalSalary)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.TotalAdditional)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.TotalGross)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.TotalDeduction)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.TotalNet)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
    }
}