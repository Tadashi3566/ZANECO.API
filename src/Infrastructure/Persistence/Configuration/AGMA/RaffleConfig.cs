using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.AGMA;

public class RaffleConfig : IEntityTypeConfiguration<Raffle>
{
    public void Configure(EntityTypeBuilder<Raffle> builder)
    {
        //_ = builder.ToTable("Raffles", SchemaNames.ZANECO)
        _ = builder.IsMultiTenant();

        _ = builder.Property(b => b.Name)
            .IsRequired();

        _ = builder.Property(b => b.RaffleDate)
            .HasColumnType("datetime(6)");
    }
}