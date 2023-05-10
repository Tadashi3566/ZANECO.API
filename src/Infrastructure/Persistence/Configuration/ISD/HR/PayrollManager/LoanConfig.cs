using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.PayrollManager;

internal class LoanConfig : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        _ = builder.Property(b => b.Amount)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.Ammortization)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.DateReleased)
            .HasColumnType("date");
        _ = builder.Property(b => b.StartDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.EndDate)
            .HasColumnType("date");
    }
}