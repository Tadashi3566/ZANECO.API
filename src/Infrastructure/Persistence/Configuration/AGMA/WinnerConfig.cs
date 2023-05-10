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

        _ = builder.Property(b => b.Name)
            .HasMaxLength(1024);
    }
}