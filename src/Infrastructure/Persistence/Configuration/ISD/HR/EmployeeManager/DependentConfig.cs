using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.EmployeeManager;

internal class DependentConfig : IEntityTypeConfiguration<Dependent>
{
    public void Configure(EntityTypeBuilder<Dependent> builder)
    {
        _ = builder.ToTable("Dependents", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(1024);
        _ = builder.Property(b => b.Gender)
            .IsRequired()
            .HasMaxLength(8);
        _ = builder.Property(b => b.Relation)
            .IsRequired()
            .HasMaxLength(32);
        _ = builder.Property(b => b.BirthDate)
            .HasColumnType("date");
    }
}