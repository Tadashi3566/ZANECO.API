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
            .IsMultiTenant();

        _ = builder.Property(b => b.ManagerId)
            .IsRequired();

        _ = builder.Property(b => b.ManagerName)
            .IsRequired()
            .HasMaxLength(1024);

        _ = builder.Property(b => b.MemberId)
            .IsRequired();

        _ = builder.Property(b => b.MemberName)
            .IsRequired()
            .HasMaxLength(1024);
    }
}
