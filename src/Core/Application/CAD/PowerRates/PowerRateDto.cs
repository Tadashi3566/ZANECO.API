namespace ZANECO.API.Application.CAD.PowerRates;

public class PowerRateDto : DtoExtension, IDto
{
    public string Code { get; set; } = default!;
    public string BillMonth { get; set; } = default!;
    public decimal GenerationCharge { get; set; }
    public decimal HostCommCharge { get; set; }
    public decimal ForexICERADAACharge { get; set; }
    public decimal TransmissionDemandCharge { get; set; }
    public decimal TransmissionCharge { get; set; }
    public decimal SystemLossCharge { get; set; }
    public decimal DistributionSystemCharge { get; set; }
    public decimal DistributionDemandCharge { get; set; }
    public decimal SupplySystemCharge { get; set; }
    public decimal SupplyRetailCharge { get; set; }
    public decimal MeteringSystemCharge { get; set; }
    public decimal MeteringRetailCharge { get; set; }
    public decimal UniChargeStrandedDebt { get; set; }
    public decimal UniChargeStrandedContractNPC { get; set; }
    public decimal UniChargeStrandedContractDU { get; set; }
    public decimal UniChargeME { get; set; }
    public decimal UniChargeEqalTaxRoyalties { get; set; }
    public decimal UniChargeEnvironmental { get; set; }
    public decimal UniChargeCrossSubsidyRemoval { get; set; }
    public decimal InterclassCrossSubsidyCharge { get; set; }
    public decimal PowerActReductionCharge { get; set; }
    public decimal LifelineDiscountSubsidyCharge { get; set; }
    public decimal LoanCondonationCharge { get; set; }
    public decimal GRAMICERADAACharge { get; set; }
    public decimal LoanCondonationCustomerPerMonth { get; set; }
    public decimal PrevYearPowerCostAdjustment { get; set; }
    public decimal SystemLossUnderRecovery { get; set; }
    public decimal DistributionVAT { get; set; }
    public decimal GenerationVAT { get; set; }
    public decimal TransmissionVAT { get; set; }
    public decimal SystemLossVAT { get; set; }
    public decimal DifferentialBillPerKWH { get; set; }
    public decimal DifferentialBillPerKW { get; set; }
    public decimal DifferentialBillCustomerPerMonth { get; set; }
    public decimal RFSCCAPEXCharge { get; set; }
    public decimal SeniorCitizenDiscount { get; set; }
    public decimal SeniorCitizenDiscountKWH { get; set; }
    public decimal SeniorCitizenDiscountPercentage { get; set; }
    public decimal SeniorCitizenSubsidyKWH { get; set; }
    public decimal WheelingCharge { get; set; }
    public decimal OtherGenerationCharge { get; set; }
    public decimal OtherTransmissionCharge { get; set; }
    public decimal OtherTransmissionDemandCharge { get; set; }
    public decimal OtherSystemLossCharge { get; set; }
    public decimal OtherLifelineCharge { get; set; }
    public decimal OtherSeniorCitizenCharge { get; set; }
    public decimal FeedInTariffAllowance { get; set; }
    public decimal PowerActRecoveryCharge { get; set; }
    public decimal FinalVAT { get; set; }
    public decimal WithholdingTaxServices { get; set; }
    public decimal WithholdingTaxRental { get; set; }


}