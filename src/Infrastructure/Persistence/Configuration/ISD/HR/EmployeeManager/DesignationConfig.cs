using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.EmployeeManager;

internal class DesignationConfig : IEntityTypeConfiguration<Designation>
{
    public void Configure(EntityTypeBuilder<Designation> builder)
    {
        _ = builder.ToTable("Designations", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.StartDate)
            .IsRequired()
            .HasColumnType("date");
        _ = builder.Property(b => b.EndDate)
            .IsRequired()
            .HasColumnType("date");

        _ = builder.Property(b => b.Area)
            .HasMaxLength(32);
        _ = builder.Property(b => b.Department)
            .HasMaxLength(64);
        _ = builder.Property(b => b.Division)
            .HasMaxLength(128);
        _ = builder.Property(b => b.Section)
            .HasMaxLength(128);
        _ = builder.Property(b => b.Position)
            .HasMaxLength(128);

        _ = builder.Property(b => b.EmploymentType)
            .HasMaxLength(16);
        _ = builder.Property(b => b.SalaryNumber)
            .HasColumnType("int")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.SalaryName)
            .HasMaxLength(16);
        _ = builder.Property(b => b.SalaryAmount)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.RateType)
            .HasMaxLength(8);
        _ = builder.Property(b => b.RatePerDay)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.HoursPerDay)
            .HasColumnType("int")
            .HasDefaultValue(8);
        _ = builder.Property(b => b.RatePerHour)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.TaxType)
            .HasMaxLength(8);
        _ = builder.Property(b => b.PayType)
            .HasMaxLength(16);
    }
}