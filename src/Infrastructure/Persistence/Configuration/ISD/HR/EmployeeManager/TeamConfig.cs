using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.EmployeeManager;

internal class TeamConfig : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        _ = builder
            .ToTable("Teams", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.LeaderId)
            .IsRequired();

        _ = builder.Property(b => b.LeaderName)
            .IsRequired()
            .HasMaxLength(1024);

        _ = builder.Property(b => b.EmployeeId)
            .IsRequired();

        _ = builder.Property(b => b.EmployeeName)
            .IsRequired()
            .HasMaxLength(1024);

        _ = builder.Property(b => b.Department)
            .HasDefaultValue(null)
            .HasMaxLength(256);

        _ = builder.Property(b => b.Position)
            .HasDefaultValue(null)
            .HasMaxLength(256);
    }
}
