using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.PayrollManager;

internal class SalaryConfig : IEntityTypeConfiguration<Salary>
{
    public void Configure(EntityTypeBuilder<Salary> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.Name)
            .IsRequired();
        _ = builder.Property(b => b.StartDate)
            .IsRequired()
            .HasColumnType("Date");
        _ = builder.Property(b => b.EndDate)
            .IsRequired()
            .HasColumnType("Date");
        _ = builder.Property(b => b.Amount)
            .IsRequired()
            .HasColumnType("Decimal(12,2)");
        _ = builder.Property(b => b.IncrementAmount)
            .IsRequired()
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
    }
}