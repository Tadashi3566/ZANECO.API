using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.SMS;

public class ContactConfig : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.ContactType)
            .HasMaxLength(16);
        _ = builder.Property(b => b.Reference)
            .HasMaxLength(16);
        _ = builder.Property(b => b.PhoneNumber)
            .HasMaxLength(16);
        _ = builder.Property(b => b.Name)
            .IsRequired();
        _ = builder.Property(b => b.Address)
            .HasMaxLength(1024);
    }
}