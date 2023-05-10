using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.CAD;

public class AreaConfig : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> builder)
    {
        _ = builder.IsMultiTenant();
        _ = builder.Property(b => b.Number)
            .HasColumnType("int");
        _ = builder.Property(b => b.Code)
            .HasMaxLength(32);
        _ = builder.Property(b => b.Name)
            .HasMaxLength(64);
    }
}