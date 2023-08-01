using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Infrastructure.Persistence.Configuration.CAD;

public class PowerRateConfig : IEntityTypeConfiguration<PowerRate>
{
    public void Configure(EntityTypeBuilder<PowerRate> builder)
    {
        _ = builder
            .IsMultiTenant();

        _ = builder.Property(b => b.Code)
            .HasMaxLength(16);
        _ = builder.Property(b => b.BillMonth)
            .HasMaxLength(4);
        _ = builder.Property(b => b.GenerationCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.HostCommCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.ForexICERADAACharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.TransmissionDemandCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.TransmissionCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.SystemLossCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.DistributionSystemCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.DistributionDemandCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.SupplySystemCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.SupplyRetailCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.MeteringSystemCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.MeteringRetailCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.UniChargeStrandedDebt)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.UniChargeStrandedContractNPC)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.UniChargeStrandedContractDU)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.UniChargeME)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.UniChargeEqalTaxRoyalties)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.UniChargeEnvironmental)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.UniChargeCrossSubsidyRemoval)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.InterclassCrossSubsidyCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.PowerActReductionCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.LifelineDiscountSubsidyCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.LoanCondonationCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.GRAMICERADAACharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.LoanCondonationCustomerPerMonth)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.PrevYearPowerCostAdjustment)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.SystemLossUnderRecovery)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.DistributionVAT)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.GenerationVAT)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.TransmissionVAT)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.SystemLossVAT)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.DifferentialBillPerKWH)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.DifferentialBillPerKW)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.DifferentialBillCustomerPerMonth)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.RFSCCAPEXCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.SeniorCitizenDiscount)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.SeniorCitizenDiscountKWH)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.SeniorCitizenDiscountPercentage)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.SeniorCitizenSubsidyKWH)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.WheelingCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.OtherGenerationCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.OtherTransmissionCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.OtherTransmissionDemandCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.OtherSystemLossCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.OtherLifelineCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.OtherSeniorCitizenCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.FeedInTariffAllowance)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.PowerActRecoveryCharge)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.FinalVAT)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.WithholdingTaxServices)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
        _ = builder.Property(b => b.WithholdingTaxRental)
            .HasColumnType("Decimal(12,2)")
            .HasDefaultValue(0);
    }
}