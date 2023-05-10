using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.PayrollManager;

internal class EmployeePayrollConfig : IEntityTypeConfiguration<EmployeePayroll>
{
    public void Configure(EntityTypeBuilder<EmployeePayroll> builder)
    {
        _ = builder.ToTable("EmployeePayrolls", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.StartDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.EndDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.PayrollDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.Salary)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.Additional)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.Gross)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.Deduction)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.Net)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
    }
}