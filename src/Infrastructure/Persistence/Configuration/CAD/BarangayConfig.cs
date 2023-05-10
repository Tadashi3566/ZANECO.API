using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.CAD;

public class BarangayConfig : IEntityTypeConfiguration<Barangay>
{
    public void Configure(EntityTypeBuilder<Barangay> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.AreaName)
            .HasMaxLength(64);
        _ = builder.Property(b => b.Name)
            .HasMaxLength(64);
    }
}