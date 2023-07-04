using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.EmployeeManager;

internal class AppointmentConfig : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.AppointmentType)
            .IsRequired()
            .HasMaxLength(16);

        _ = builder.Property(b => b.Subject)
            .IsRequired()
            .HasMaxLength(1024);

        _ = builder.Property(b => b.StartDateTime)
            .HasColumnType("datetime(6)");

        _ = builder.Property(b => b.EndDateTime)
            .HasColumnType("datetime(6)");

        _ = builder.Property(b => b.Subject)
            .HasMaxLength(1024);
    }
}