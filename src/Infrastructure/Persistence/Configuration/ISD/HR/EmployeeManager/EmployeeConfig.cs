using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.EmployeeManager;

internal class EmployeeConfig : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        _ = builder.Property(b => b.Title)
            .HasMaxLength(8);
        _ = builder.Property(b => b.FirstName)
            .HasMaxLength(64);
        _ = builder.Property(b => b.MiddleName)
            .HasMaxLength(64);
        _ = builder.Property(b => b.LastName)
            .HasMaxLength(64);
        _ = builder.Property(b => b.Extension)
            .HasMaxLength(8);
        _ = builder.Property(b => b.Gender)
            .HasMaxLength(8);
        _ = builder.Property(b => b.BirthDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.PhoneNumber)
            .HasMaxLength(32);
        _ = builder.Property(b => b.Email)
            .HasMaxLength(64);
        _ = builder.Property(b => b.CivilStatus)
            .HasMaxLength(16);
        _ = builder.Property(b => b.HireDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.StartDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.RegularDate)
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

        _ = builder.Property(b => b.Sss)
            .HasMaxLength(16);
        _ = builder.Property(b => b.Phic)
            .HasMaxLength(16);
        _ = builder.Property(b => b.Hdmf)
            .HasMaxLength(16);
        _ = builder.Property(b => b.Tin)
            .HasMaxLength(16);

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
        _ = builder.Property(b => b.DaysPerMonth)
            .HasColumnType("int")
            .HasDefaultValue(26);
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
        _ = builder.Property(b => b.PayThrough)
            .HasMaxLength(16);

        _ = builder.Property(b => b.EmergencyNumber)
            .HasMaxLength(16);
        _ = builder.Property(b => b.EmergencyRelation)
            .HasMaxLength(16);

        _ = builder.Property(b => b.BloodType)
            .HasMaxLength(4);
    }
}