using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.EmployeeManager;

internal class JobDescriptionConfig : IEntityTypeConfiguration<JobDescription>
{
    public void Configure(EntityTypeBuilder<JobDescription> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.Rank)
            .IsRequired();

        _ = builder.Property(b => b.Number)
            .IsRequired();

        _ = builder.Property(b => b.Department)
            .IsRequired()
            .HasMaxLength(64);

        _ = builder.Property(b => b.ReportsTo)
            .IsRequired()
            .HasMaxLength(128);
    }
}