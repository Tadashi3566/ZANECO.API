using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.PayrollManager;

internal class ScheduleDetailConfig : IEntityTypeConfiguration<ScheduleDetail>
{
    public void Configure(EntityTypeBuilder<ScheduleDetail> builder)
    {
        _ = builder.Property(b => b.Day)
            .IsRequired()
            .HasMaxLength(16);
        _ = builder.Property(b => b.TimeIn1)
            .IsRequired()
            .HasMaxLength(8);
        _ = builder.Property(b => b.TimeOut1)
            .IsRequired()
            .HasMaxLength(8);
        _ = builder.Property(b => b.TimeIn2)
            .IsRequired()
            .HasMaxLength(8);
        _ = builder.Property(b => b.TimeOut2)
            .IsRequired()
            .HasMaxLength(8);
        _ = builder.Property(b => b.TotalHours)
            .IsRequired()
            .HasColumnType("int");
    }
}