using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.App;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.App;

public class GroupConfig : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.Application)
            .HasMaxLength(64);
        _ = builder.Property(b => b.Parent)
            .HasMaxLength(64);
        _ = builder.Property(b => b.Tag)
            .HasMaxLength(64)
            .HasDefaultValue(string.Empty);
        _ = builder.Property(b => b.Code)
            .HasMaxLength(64);
        _ = builder.Property(b => b.Name)
            .HasMaxLength(64);
        _ = builder.Property(b => b.Amount)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.Manager)
            .HasMaxLength(1024);
    }
}