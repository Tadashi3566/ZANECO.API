using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.EmployeeManager;

internal class EmployerConfig : IEntityTypeConfiguration<Employer>
{
    public void Configure(EntityTypeBuilder<Employer> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.Designation)
            .IsRequired()
            .HasMaxLength(64);
        _ = builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(1024);
        _ = builder.Property(b => b.Address)
            .IsRequired()
            .HasMaxLength(1024);
        _ = builder.Property(b => b.StartDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.EndDate)
            .HasColumnType("date");
    }
}