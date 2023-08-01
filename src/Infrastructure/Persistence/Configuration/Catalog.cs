using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.Catalog;

namespace ZANECO.API.Infrastructure.Persistence.Configuration;

public class BrandConfig : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        _ = builder.ToTable("Brands", SchemaNames.Catalog)
            .IsMultiTenant();
    }
}

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        _ = builder.ToTable("Products", SchemaNames.Catalog)
            .IsMultiTenant();

        builder
            .Property(p => p.ImagePath)
                .HasMaxLength(2048);
    }
}