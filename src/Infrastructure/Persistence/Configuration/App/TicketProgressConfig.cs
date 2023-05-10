using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.App;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.App;

public class TicketProgressConfig : IEntityTypeConfiguration<TicketProgress>
{
    public void Configure(EntityTypeBuilder<TicketProgress> builder)
    {
        _ = builder.ToTable("TicketProgress", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.ProgressType)
            .HasMaxLength(16);
        _ = builder.Property(b => b.Name)
            .HasMaxLength(1024);
    }
}