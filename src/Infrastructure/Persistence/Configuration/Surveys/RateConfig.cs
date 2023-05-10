using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.Surveys;

public class RateConfig : IEntityTypeConfiguration<Rate>
{
    public void Configure(EntityTypeBuilder<Rate> builder)
    {
        _ = builder.ToTable("Rates", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.Number)
            .HasColumnType("int");
        _ = builder.Property(b => b.Name)
            .HasMaxLength(16);
    }
}