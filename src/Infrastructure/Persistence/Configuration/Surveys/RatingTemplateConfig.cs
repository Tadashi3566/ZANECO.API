using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.Surveys;

public class RatingTemplateConfig : IEntityTypeConfiguration<RatingTemplate>
{
    public void Configure(EntityTypeBuilder<RatingTemplate> builder)
    {
        _ = builder.Property(b => b.Comment)
            .HasMaxLength(1024);
    }
}