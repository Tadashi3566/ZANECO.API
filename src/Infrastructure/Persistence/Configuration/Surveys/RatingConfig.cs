using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.Surveys;

public class RatingConfig : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        _ = builder.Property(b => b.RateNumber)
            .HasColumnType("int");
        _ = builder.Property(b => b.Comment)
            .HasMaxLength(1024);
    }
}