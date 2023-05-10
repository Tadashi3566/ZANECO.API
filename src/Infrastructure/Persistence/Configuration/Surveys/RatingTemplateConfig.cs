using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.Surveys;

public class RatingTemplateConfig : IEntityTypeConfiguration<RatingTemplate>
{
    public void Configure(EntityTypeBuilder<RatingTemplate> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.Comment)
            .HasMaxLength(1024);
    }
}