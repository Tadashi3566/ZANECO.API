using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.PayrollManager;

internal class EmployeePayrollDetailConfig : IEntityTypeConfiguration<EmployeePayrollDetail>
{
    public void Configure(EntityTypeBuilder<EmployeePayrollDetail> builder)
    {
        _ = builder.Property(b => b.StartDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.EndDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.PayrollDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.Amount)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
    }
}