using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.EmployeeManager;

internal class AttendanceConfig : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        _ = builder.ToTable("Attendance", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.EmployeeName)
            .IsRequired()
            .HasMaxLength(1024);
        _ = builder.Property(b => b.DayType)
            .IsRequired()
            .HasMaxLength(16);
        _ = builder.Property(b => b.AttendanceDate)
            .IsRequired()
            .HasColumnType("date");
        _ = builder.Property(b => b.ScheduleDetailDay)
            .HasMaxLength(16);
        _ = builder.Property(b => b.LateMinutes)
            .HasColumnType("int");
        _ = builder.Property(b => b.UnderTimeMinutes)
            .HasColumnType("int");
        _ = builder.Property(b => b.TotalHours)
            .HasColumnType("Double(12,2");
        _ = builder.Property(b => b.PaidHours)
            .HasColumnType("Double(12,2");
    }
}