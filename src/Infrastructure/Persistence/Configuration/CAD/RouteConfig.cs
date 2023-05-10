using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.CAD;

public class RouteConfig : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        _ = builder.ToTable("Routes", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.AreaId)
            .HasMaxLength(36);
        _ = builder.Property(b => b.AreaName)
            .HasMaxLength(1024);
        _ = builder.Property(b => b.Number)
            .HasColumnType("int");
        _ = builder.Property(b => b.Code)
            .HasMaxLength(16);
        _ = builder.Property(b => b.Name)
            .HasMaxLength(1024);
    }
}