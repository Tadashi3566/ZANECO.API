using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.SMS;

public class MessageTemplateConfig : IEntityTypeConfiguration<MessageTemplate>
{
    public void Configure(EntityTypeBuilder<MessageTemplate> builder)
    {
        _ = builder.ToTable("MessageTemplates", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.TemplateType)
            .HasMaxLength(32);
        _ = builder.Property(b => b.MessageType)
            .HasMaxLength(32);
        _ = builder.Property(b => b.Subject)
            .HasMaxLength(160);
        _ = builder.Property(b => b.Message)
            .HasMaxLength(1500);
        _ = builder.Property(b => b.Schedule)
            .HasColumnType("datetime(6)");
    }
}