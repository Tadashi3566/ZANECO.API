using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.AGMA;

public class WinnerConfig : IEntityTypeConfiguration<Winner>
{
    public void Configure(EntityTypeBuilder<Winner> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.RaffleId)
            .IsRequired();

        _ = builder.Property(b => b.RaffleName)
            .IsRequired()
            .HasMaxLength(1024);

        _ = builder.Property(b => b.PrizeId)
            .IsRequired();

        _ = builder.Property(b => b.PrizeName)
            .IsRequired()
            .HasMaxLength(1024);

        _ = builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(1024);

        _ = builder.Property(b => b.Address)
            .IsRequired()
            .HasMaxLength(1024);
    }
}