﻿using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.ISD.HR.PayrollManager;

internal class EmployeeAdjustmentConfig : IEntityTypeConfiguration<EmployeeAdjustment>
{
    public void Configure(EntityTypeBuilder<EmployeeAdjustment> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.EmployeeName)
            .IsRequired()
            .HasMaxLength(1024);
        _ = builder.Property(b => b.AdjustmentType)
            .IsRequired()
            .HasMaxLength(16);
        _ = builder.Property(b => b.PaymentSchedule)
            .IsRequired()
            .HasMaxLength(16);
        _ = builder.Property(b => b.AdjustmentName)
            .IsRequired()
            .HasMaxLength(64);
        _ = builder.Property(b => b.Amount)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.StartDate)
            .HasColumnType("date");
        _ = builder.Property(b => b.EndDate)
            .HasColumnType("date");
    }
}