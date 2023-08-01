using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.EmployeeManager;

internal class DocumentConfig : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.DocumentDate)
            .HasColumnType("datetime(6)");
        _ = builder.Property(b => b.DocumentType)
            .HasMaxLength(32);
        _ = builder.Property(b => b.Reference)
            .HasMaxLength(32);
        _ = builder.Property(b => b.Name)
            .IsRequired();
    }
}