using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.CAD;

public class RemoteCollectionConfig : IEntityTypeConfiguration<RemoteCollection>
{
    public void Configure(EntityTypeBuilder<RemoteCollection> builder)
    {
        _ = builder.IsMultiTenant();

        _ = builder.Property(b => b.Collector)
            .IsRequired()
            .HasMaxLength(32);
        _ = builder.Property(b => b.Reference)
            .IsRequired()
            .HasMaxLength(16);
        _ = builder.Property(b => b.TransactionDate)
            .IsRequired()
            .HasColumnType("datetime(6)");
        _ = builder.Property(b => b.ReportDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.AccountNumber)
            .IsRequired()
            .HasMaxLength(10);
        _ = builder.Property(b => b.Name)
            .HasMaxLength(1024);
        _ = builder.Property(b => b.Amount)
            .IsRequired()
            .HasColumnType("Decimal(12,2)");
        _ = builder.Property(b => b.OrNumber)
            .HasMaxLength(16);
    }
}