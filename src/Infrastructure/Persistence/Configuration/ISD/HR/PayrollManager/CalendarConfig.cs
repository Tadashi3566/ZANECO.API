﻿using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.PayrollManager;

internal class CalendarConfig : IEntityTypeConfiguration<Calendar>
{
    public void Configure(EntityTypeBuilder<Calendar> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.CalendarType)
            .IsRequired()
            .HasMaxLength(16);
        _ = builder.Property(b => b.CalendarDate)
            .IsRequired()
            .HasColumnType("date");
        _ = builder.Property(b => b.Day)
            .IsRequired()
            .HasMaxLength(16);
        _ = builder.Property(b => b.Name)
            .IsRequired();
    }
}