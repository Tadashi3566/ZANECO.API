using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.CAD;

public class AccountConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.IdCode)
            .HasColumnType("int")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.AccountNumber)
            .HasMaxLength(16);
        _ = builder.Property(b => b.Area)
            .HasMaxLength(4);
        _ = builder.Property(b => b.Route)
            .HasMaxLength(8);
        _ = builder.Property(b => b.Cipher)
            .HasMaxLength(8);
        _ = builder.Property(b => b.Tag)
            .HasMaxLength(32);
        _ = builder.Property(b => b.Name)
            .IsRequired();
        _ = builder.Property(b => b.Address)
            .HasMaxLength(1024);

        _ = builder.Property(b => b.AccountType)
            .HasMaxLength(16);
        _ = builder.Property(b => b.Feeder)
            .HasMaxLength(16);
        _ = builder.Property(b => b.Pole)
            .HasMaxLength(32);
        _ = builder.Property(b => b.Transformer)
            .HasMaxLength(32);
        _ = builder.Property(b => b.MeterBrand)
            .HasMaxLength(32);
        _ = builder.Property(b => b.MeterSerial)
            .HasMaxLength(32);

        _ = builder.Property(b => b.ConnectionStatus)
            .HasMaxLength(16);

        _ = builder.Property(b => b.ConnectionDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.DisconnectionDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.ReconnectionDate)
            .HasColumnType("date");

        _ = builder.Property(b => b.BillMonth)
            .HasMaxLength(4);

        _ = builder.Property(b => b.PreviousReadingDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.PreviousReadingKWH)
            .HasColumnType("double")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.PresentReadingDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.PresentReadingKWH)
            .HasColumnType("double")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.UsedKWH)
            .HasColumnType("double")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.UsedKWH)
            .HasColumnType("double")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.Multiplier)
            .HasColumnType("int")
            .HasDefaultValue(0);

        _ = builder.Property(b => b.BillNumber)
            .HasMaxLength(16);
        _ = builder.Property(b => b.BillAmount)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);

        _ = builder.Property(b => b.PreviousReadingKWHCM)
            .HasColumnType("double")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.PresentReadingKWHCM)
            .HasColumnType("double")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.UsedKWHCM)
            .HasColumnType("double")
            .HasDefaultValue(0);
    }
}