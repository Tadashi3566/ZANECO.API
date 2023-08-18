using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.CAD;

public class LedgerConfig : IEntityTypeConfiguration<Ledger>
{
    public void Configure(EntityTypeBuilder<Ledger> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.IdCode)
            .HasMaxLength(8);
        _ = builder.Property(b => b.AccountNumber)
            .HasMaxLength(16);
        _ = builder.Property(b => b.BillMonth)
            .HasMaxLength(4);
        _ = builder.Property(b => b.BillNumber)
            .HasMaxLength(16);
        _ = builder.Property(b => b.LastReading)
            .HasColumnType("double");
        _ = builder.Property(b => b.KWH)
            .HasColumnType("double");

        _ = builder.Property(b => b.UCNPCSD)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.UCNPCSCC)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.UCDUSCC)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.UCME)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.UCETR)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.UCEC)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.UCCSR)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);

        _ = builder.Property(b => b.VATDistribution)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.VATGeneration)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.VATTransmission)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.VATSLGeneration)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.VATSLTransmission)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.VAT)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.VATDiscount)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);

        _ = builder.Property(b => b.Debit)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.Credit)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.Balance)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);

        _ = builder.Property(b => b.Collector)
            .HasMaxLength(16);
        _ = builder.Property(b => b.PostingDate)
            .HasColumnType("datetime(6)");
    }
}