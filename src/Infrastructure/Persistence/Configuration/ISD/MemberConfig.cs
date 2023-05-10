using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD;

public class MemberConfig : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        _ = builder.ToTable("Members", SchemaNames.ZANECO)
            .IsMultiTenant();

        _ = builder.Property(b => b.Name)
            .HasMaxLength(2024);
        _ = builder.Property(b => b.Address)
            .HasMaxLength(1024);
        _ = builder.Property(b => b.District)
            .HasMaxLength(4);
        _ = builder.Property(b => b.Municipality)
            .HasMaxLength(16);
        _ = builder.Property(b => b.Barangay)
            .HasMaxLength(32);
        _ = builder.Property(b => b.Gender)
            .HasMaxLength(8);
        _ = builder.Property(b => b.PhoneNumber)
            .HasMaxLength(16);
        _ = builder.Property(b => b.BirthDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.ApplicationDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.MembershipDate)
            .HasColumnType("date");
    }
}