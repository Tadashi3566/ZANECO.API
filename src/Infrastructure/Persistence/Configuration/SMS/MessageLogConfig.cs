using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.SMS;

public class MessageLogConfig : IEntityTypeConfiguration<MessageLog>
{
    public void Configure(EntityTypeBuilder<MessageLog> builder)
    {
        _ = builder.Property(b => b.MessageFrom)
            .IsRequired()
            .HasMaxLength(16);

        _ = builder.Property(b => b.MessageTo)
            .IsRequired()
            .HasMaxLength(16);

        _ = builder.Property(b => b.MessageHash)
            .HasMaxLength(64);

        _ = builder.Property(b => b.MessageText)
            .HasMaxLength(1500);

        _ = builder.Property(b => b.StatusCode)
            .HasColumnType("int")
            .HasDefaultValue(0);
    }
}