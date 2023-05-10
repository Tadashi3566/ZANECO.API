using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.App;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.App;

public class TicketConfig : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        _ = builder.ToTable("Tickets", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.Name)
            .HasMaxLength(1024);
        _ = builder.Property(b => b.Impact)
            .HasDefaultValue("LOW")
            .HasMaxLength(16);
        _ = builder.Property(b => b.Urgency)
            .HasDefaultValue("LOW")
            .HasMaxLength(16);
        _ = builder.Property(b => b.Priority)
            .HasDefaultValue("LOW")
            .HasMaxLength(16);

        _ = builder.Property(b => b.OpenedBy)
            .HasMaxLength(36);
        _ = builder.Property(b => b.OpenedOn)
            .HasColumnType("datetime(6)");
        _ = builder.Property(b => b.SuspendedBy)
            .HasMaxLength(36);
        _ = builder.Property(b => b.SuspendedOn)
            .HasColumnType("datetime(6)");
        _ = builder.Property(b => b.ClosedBy)
            .HasMaxLength(36);
        _ = builder.Property(b => b.ClosedOn)
            .HasColumnType("datetime(6)");
        _ = builder.Property(b => b.ApprovedBy)
            .HasMaxLength(36);
        _ = builder.Property(b => b.ApprovedOn)
            .HasColumnType("datetime(6)");
        _ = builder.Property(b => b.Status)
            .HasDefaultValue("PENDING")
            .HasMaxLength(16);
    }
}