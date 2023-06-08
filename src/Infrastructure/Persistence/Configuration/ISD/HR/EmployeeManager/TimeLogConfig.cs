using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.EmployeeManager;

internal class TimeLogConfig : IEntityTypeConfiguration<TimeLog>
{
    public void Configure(EntityTypeBuilder<TimeLog> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.Device)
            .HasMaxLength(64);
        _ = builder.Property(b => b.LogType)
            .IsRequired()
            .HasMaxLength(8);
        _ = builder.Property(b => b.LogDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.LogDateTime)
            .HasColumnType("datetime(6)");
        _ = builder.Property(b => b.SyncDateTime)
            .HasColumnType("datetime(6)");
    }
}