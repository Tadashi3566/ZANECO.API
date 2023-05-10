using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.EmployeeManager;

internal class TimeLogConfig : IEntityTypeConfiguration<TimeLog>
{
    public void Configure(EntityTypeBuilder<TimeLog> builder)
    {
        _ = builder.ToTable("TimeLogs", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.LogType)
            .IsRequired()
            .HasMaxLength(8);
        _ = builder.Property(b => b.LogDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.LogDateTime)
            .HasColumnType("datetime(6)");
    }
}