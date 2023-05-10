using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.PayrollManager;

internal class ContributionConfig : IEntityTypeConfiguration<Contribution>
{
    public void Configure(EntityTypeBuilder<Contribution> builder)
    {
        _ = builder.ToTable("Contributions", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.StartDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.EndDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.RangeStart)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.RangeEnd)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.EmployerContribution)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.EmployeeContribution)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.TotalContribution)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.Percentage)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
    }
}