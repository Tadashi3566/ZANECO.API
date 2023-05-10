using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.SMS;

public class MessageLogConfig : IEntityTypeConfiguration<MessageLog>
{
    public void Configure(EntityTypeBuilder<MessageLog> builder)
    {
        _ = builder.Property(b => b.MessageFrom)
            .HasMaxLength(16);
        _ = builder.Property(b => b.MessageTo)
            .HasMaxLength(16);
        _ = builder.Property(b => b.MessageText)
            .HasMaxLength(1500);
    }
}