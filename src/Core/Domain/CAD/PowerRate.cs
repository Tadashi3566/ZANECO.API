using ZANECO.API.Domain.App;

namespace ZANECO.API.Domain.CAD;

public class PowerRate : AuditableEntity, IAggregateRoot
{
    public PowerRate()
    {
    }

    public virtual Group Group { get; private set; } = default!;
    public Guid GroupId { get; private set; }
    public string Code { get; private set; } = default!; // rate code
    public string Name { get; private set; } = default!; // rate name
    public string BillMonth { get; private set; } = default!; // Billing Month
    public decimal GenerationCharge { get; private set; } // Adjusted Generation Rate
    public decimal HostCommCharge { get; private set; } // Franchise & Benefits to Host Comm Charge
    public decimal ForexICERADAACharge { get; private set; } // Forex/ICERA ICERA/DAA
    public decimal TransmissionDemandCharge { get; private set; } // Adjusted transmission Demand Rate
    public decimal TransmissionCharge { get; private set; } // Adjusted Transmission Rate
    public decimal SystemLossCharge { get; private set; } // System Loss Rate
    public decimal DistributionSystemCharge { get; private set; } // Distribution System Charge
    public decimal DistributionDemandCharge { get; private set; } // Distribution Charge: Demand Charge
    public decimal SupplySystemCharge { get; private set; } // Supply Charges: Supply System Charge
    public decimal SupplyRetailCharge { get; private set; } // Supply Charges: Retail Customer Charge
    public decimal MeteringSystemCharge { get; private set; } // Metering Charges: Metering System Charge
    public decimal MeteringRetailCharge { get; private set; } // Metering Charges: Retail Customer Charge
    public decimal UniChargeStrandedDebt { get; private set; } // Universal Charges: Stranded Debts
    public decimal UniChargeStrandedContractNPC { get; private set; } // Universal Charges: NPC Stranded Contract Cost
    public decimal UniChargeStrandedContractDU { get; private set; } // Universal Charges: DU's Stranded Contract Cost
    public decimal UniChargeME { get; private set; } // Universal Charges: Missionary Electrification Charge
    public decimal UniChargeEqalTaxRoyalties { get; private set; } // Universal Charges: Equalization of Taxes and Royalties
    public decimal UniChargeEnvironmental { get; private set; } // Universal Charges: Environmental
    public decimal UniChargeCrossSubsidyRemoval { get; private set; } // Universal Charges: Cross Subsidy Removal
    public decimal InterclassCrossSubsidyCharge { get; private set; } // Interclass Cross Subsidy Adjustment
    public decimal PowerActReductionCharge { get; private set; } // Power Act Reduction
    public decimal LifelineDiscountSubsidyCharge { get; private set; } // Lifeline Rate (TotalDiscount) Subsidy
    public decimal LoanCondonationCharge { get; private set; } // Loan Condonation
    public decimal GRAMICERADAACharge { get; private set; } // GRAM & ICERA DAA
    public decimal LoanCondonationCustomerPerMonth { get; private set; } // Loan Condonation/ Customer/ Month
    public decimal PrevYearPowerCostAdjustment { get; private set; } // Previous Year Power Cost Adjustment
    public decimal SystemLossUnderRecovery { get; private set; } // System Loss Under Recovery
    public decimal DistributionVAT { get; private set; } // EVAT Distribution
    public decimal GenerationVAT { get; private set; } // Generation TotalVat Content
    public decimal TransmissionVAT { get; private set; } // Transmission TotalVat Content
    public decimal SystemLossVAT { get; private set; } // System Loss VAT
    public decimal DifferentialBillPerKWH { get; private set; } // Differential Billing Rate per Kilowatt Hour
    public decimal DifferentialBillPerKW { get; private set; } // Differential Billing Rate Kilowatt
    public decimal DifferentialBillCustomerPerMonth { get; private set; } // Differential Billing Per Customer Per Month
    public decimal RFSCCAPEXCharge { get; private set; } // RFSC Charge: Reinvestment Fund for Sutainable CAPEX
    public decimal SeniorCitizenDiscount { get; private set; } // Apply Senior Citizen TotalDiscount
    public decimal SeniorCitizenDiscountKWH { get; private set; } // Senior Citizen TotalDiscount KilowattHour Reference
    public decimal SeniorCitizenDiscountPercentage { get; private set; } // Senior TotalDiscount Percentage
    public decimal SeniorCitizenSubsidyKWH { get; private set; } // Senior Citizen Subsidy Rate/KWH
    public decimal WheelingCharge { get; private set; } // Wheeling Rates Apply
    public decimal OtherGenerationCharge { get; private set; } // Other Generation Rate Adjustment
    public decimal OtherTransmissionCharge { get; private set; } // Other Transmission Cost Adjustment
    public decimal OtherTransmissionDemandCharge { get; private set; } // Other Transmission Demand Cost Adjustment
    public decimal OtherSystemLossCharge { get; private set; } // Other System Loss Cost Adjustment
    public decimal OtherLifelineCharge { get; private set; } // Other Lifeline Rate Cost Adjustment
    public decimal OtherSeniorCitizenCharge { get; private set; } // Other Senior Citizen Rate Adjustment
    public decimal FeedInTariffAllowance { get; private set; } // Feed-in Tariff Allowance (FIT-ALL)
    public decimal PowerActRecoveryCharge { get; private set; } // Power Act Rate Recovery
    public decimal FinalVAT { get; private set; } // Final VAT %
    public decimal WithholdingTaxServices { get; private set; } // Withholding Tax (Services) %
    public decimal WithholdingTaxRental { get; private set; } // Withholding Tax (Rental) %

    public PowerRate(Guid groupId, string code, string name, string billMonth, decimal generationCharge, decimal hostCommCharge, decimal forexICERADAACharge, decimal transmissionDemandCharge, decimal transmissionCharge, decimal systemLossCharge, decimal distributionSystemCharge, decimal distributionDemandCharge, decimal supplySystemCharge, decimal supplyRetailCharge, decimal meteringSystemCharge, decimal meteringRetailCharge, decimal uniChargeStrandedDebt, decimal uniChargeStrandedContractNPC, decimal uniChargeStrandedContractDU, decimal uniChargeME, decimal uniChargeEqalTaxRoyalties, decimal uniChargeEnvironmental, decimal uniChargeCrossSubsidyRemoval, decimal interclassCrossSubsidyCharge, decimal powerActReductionCharge, decimal lifelineDiscountSubsidyCharge, decimal loanCondonationCharge, decimal gRAMICERADAACharge, decimal loanCondonationCustomerPerMonth, decimal prevYearPowerCostAdjustment, decimal systemLossUnderRecovery, decimal distributionVAT, decimal generationVAT, decimal transmissionVAT, decimal systemLossVAT, decimal differentialBillPerKWH, decimal differentialBillPerKW, decimal differentialBillCustomerPerMonth, decimal rFSCCAPEXCharge, decimal seniorCitizenDiscount, decimal seniorCitizenDiscountKWH, decimal seniorCitizenDiscountPercentage, decimal seniorCitizenSubsidyKWH, decimal wheelingCharge, decimal otherGenerationCharge, decimal otherTransmissionCharge, decimal otherTransmissionDemandCharge, decimal otherSystemLossCharge, decimal otherLifelineCharge, decimal otherSeniorCitizenCharge, decimal feedInTariffAllowance, decimal powerActRecoveryCharge, decimal finalVAT, decimal withholdingTaxServices, decimal withholdingTaxRental, string? description = "", string? notes = "")
    {
        GroupId = groupId;
        Code = code;
        Name = name.Trim().ToUpper();

        BillMonth = billMonth;
        GenerationCharge = generationCharge;
        HostCommCharge = hostCommCharge;
        ForexICERADAACharge = forexICERADAACharge;
        TransmissionDemandCharge = transmissionDemandCharge;
        TransmissionCharge = transmissionCharge;
        SystemLossCharge = systemLossCharge;
        DistributionSystemCharge = distributionSystemCharge;
        DistributionDemandCharge = distributionDemandCharge;
        SupplySystemCharge = supplySystemCharge;
        SupplyRetailCharge = supplyRetailCharge;
        MeteringSystemCharge = meteringSystemCharge;
        MeteringRetailCharge = meteringSystemCharge;
        UniChargeStrandedDebt = uniChargeStrandedDebt;
        UniChargeStrandedContractNPC = uniChargeStrandedContractNPC;
        UniChargeStrandedContractDU = uniChargeStrandedContractDU;
        UniChargeME = uniChargeME;
        UniChargeEqalTaxRoyalties = uniChargeEqalTaxRoyalties;
        UniChargeEnvironmental = uniChargeEnvironmental;
        UniChargeCrossSubsidyRemoval = uniChargeCrossSubsidyRemoval;
        InterclassCrossSubsidyCharge = interclassCrossSubsidyCharge;
        PowerActReductionCharge = powerActReductionCharge;
        LifelineDiscountSubsidyCharge = lifelineDiscountSubsidyCharge;
        LoanCondonationCharge = loanCondonationCharge;
        GRAMICERADAACharge = gRAMICERADAACharge;
        LoanCondonationCustomerPerMonth = loanCondonationCustomerPerMonth;
        PrevYearPowerCostAdjustment = prevYearPowerCostAdjustment;
        SystemLossUnderRecovery = systemLossUnderRecovery;
        DistributionVAT = distributionVAT;
        GenerationVAT = generationVAT;
        TransmissionVAT = transmissionVAT;
        SystemLossVAT = systemLossVAT;
        DifferentialBillPerKWH = differentialBillPerKWH;
        DifferentialBillPerKW = differentialBillPerKW;
        DifferentialBillCustomerPerMonth = differentialBillCustomerPerMonth;
        RFSCCAPEXCharge = rFSCCAPEXCharge;
        SeniorCitizenDiscount = seniorCitizenDiscount;
        SeniorCitizenDiscountKWH = seniorCitizenDiscountKWH;
        SeniorCitizenDiscountPercentage = seniorCitizenDiscountPercentage;
        SeniorCitizenSubsidyKWH = seniorCitizenDiscountPercentage;
        WheelingCharge = wheelingCharge;
        OtherGenerationCharge = otherGenerationCharge;
        OtherTransmissionCharge = otherTransmissionCharge;
        OtherTransmissionDemandCharge = otherTransmissionDemandCharge;
        OtherSystemLossCharge = otherSystemLossCharge;
        OtherLifelineCharge = otherLifelineCharge;
        OtherSeniorCitizenCharge = otherSeniorCitizenCharge;
        FeedInTariffAllowance = feedInTariffAllowance;
        PowerActRecoveryCharge = powerActRecoveryCharge;
        FinalVAT = finalVAT;
        WithholdingTaxServices = withholdingTaxServices;
        WithholdingTaxRental = withholdingTaxRental;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
    }

    public PowerRate Update(string name, string billMonth, decimal generationCharge, decimal hostCommCharge, decimal forexICERADAACharge, decimal transmissionDemandCharge, decimal transmissionCharge, decimal systemLossCharge, decimal distributionSystemCharge, decimal distributionDemandCharge, decimal supplySystemCharge, decimal supplyRetailCharge, decimal meteringSystemCharge, decimal meteringRetailCharge, decimal uniChargeStrandedDebt, decimal uniChargeStrandedContractNPC, decimal uniChargeStrandedContractDU, decimal uniChargeME, decimal uniChargeEqalTaxRoyalties, decimal uniChargeEnvironmental, decimal uniChargeCrossSubsidyRemoval, decimal interclassCrossSubsidyCharge, decimal powerActReductionCharge, decimal lifelineDiscountSubsidyCharge, decimal loanCondonationCharge, decimal gRAMICERADAACharge, decimal loanCondonationCustomerPerMonth, decimal prevYearPowerCostAdjustment, decimal systemLossUnderRecovery, decimal distributionVAT, decimal generationVAT, decimal transmissionVAT, decimal systemLossVAT, decimal differentialBillPerKWH, decimal differentialBillPerKW, decimal differentialBillCustomerPerMonth, decimal rFSCCAPEXCharge, decimal seniorCitizenDiscount, decimal seniorCitizenDiscountKWH, decimal seniorCitizenDiscountPercentage, decimal seniorCitizenSubsidyKWH, decimal wheelingCharge, decimal otherGenerationCharge, decimal otherTransmissionCharge, decimal otherTransmissionDemandCharge, decimal otherSystemLossCharge, decimal otherLifelineCharge, decimal otherSeniorCitizenCharge, decimal feedInTariffAllowance, decimal powerActRecoveryCharge, decimal finalVAT, decimal withholdingTaxServices, decimal withholdingTaxRental, string? description = "", string? notes = "")
    {
        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();
        if (billMonth is not null && !BillMonth.Equals(billMonth)) BillMonth = billMonth;

        if (!GenerationCharge.Equals(generationCharge)) GenerationCharge = generationCharge;
        if (!HostCommCharge.Equals(hostCommCharge)) HostCommCharge = hostCommCharge;
        if (!ForexICERADAACharge.Equals(forexICERADAACharge)) ForexICERADAACharge = forexICERADAACharge;
        if (!TransmissionDemandCharge.Equals(transmissionDemandCharge)) TransmissionDemandCharge = transmissionDemandCharge;
        if (!TransmissionCharge.Equals(transmissionCharge)) TransmissionCharge = transmissionCharge;
        if (!SystemLossCharge.Equals(systemLossCharge)) SystemLossCharge = systemLossCharge;
        if (!DistributionSystemCharge.Equals(distributionSystemCharge)) DistributionSystemCharge = distributionSystemCharge;
        if (!DistributionDemandCharge.Equals(distributionDemandCharge)) DistributionDemandCharge = distributionDemandCharge;
        if (!SupplySystemCharge.Equals(supplySystemCharge)) SupplySystemCharge = supplySystemCharge;
        if (!SupplyRetailCharge.Equals(supplyRetailCharge)) SupplyRetailCharge = supplyRetailCharge;
        if (!MeteringSystemCharge.Equals(meteringSystemCharge)) MeteringSystemCharge = meteringSystemCharge;
        if (!MeteringRetailCharge.Equals(meteringRetailCharge)) MeteringRetailCharge = meteringRetailCharge;
        if (!UniChargeStrandedDebt.Equals(uniChargeStrandedDebt)) UniChargeStrandedDebt = uniChargeStrandedDebt;
        if (!UniChargeStrandedContractNPC.Equals(uniChargeStrandedContractNPC)) UniChargeStrandedContractNPC = uniChargeStrandedContractNPC;
        if (!UniChargeStrandedContractDU.Equals(uniChargeStrandedContractDU)) UniChargeStrandedContractDU = uniChargeStrandedContractDU;
        if (!UniChargeME.Equals(uniChargeME)) UniChargeME = uniChargeME;
        if (!UniChargeEqalTaxRoyalties.Equals(uniChargeEqalTaxRoyalties)) UniChargeEqalTaxRoyalties = uniChargeEqalTaxRoyalties;
        if (!UniChargeEnvironmental.Equals(uniChargeEnvironmental)) UniChargeEnvironmental = uniChargeEnvironmental;
        if (!UniChargeCrossSubsidyRemoval.Equals(uniChargeCrossSubsidyRemoval)) UniChargeCrossSubsidyRemoval = uniChargeCrossSubsidyRemoval;
        if (!InterclassCrossSubsidyCharge.Equals(interclassCrossSubsidyCharge)) InterclassCrossSubsidyCharge = interclassCrossSubsidyCharge;
        if (!PowerActReductionCharge.Equals(powerActReductionCharge)) PowerActReductionCharge = powerActReductionCharge;
        if (!LifelineDiscountSubsidyCharge.Equals(lifelineDiscountSubsidyCharge)) LifelineDiscountSubsidyCharge = lifelineDiscountSubsidyCharge;
        if (!LoanCondonationCharge.Equals(loanCondonationCharge)) LoanCondonationCharge = loanCondonationCharge;
        if (!GRAMICERADAACharge.Equals(gRAMICERADAACharge)) GRAMICERADAACharge = gRAMICERADAACharge;
        if (!LoanCondonationCustomerPerMonth.Equals(loanCondonationCustomerPerMonth)) LoanCondonationCustomerPerMonth = loanCondonationCustomerPerMonth;
        if (!PrevYearPowerCostAdjustment.Equals(prevYearPowerCostAdjustment)) PrevYearPowerCostAdjustment = prevYearPowerCostAdjustment;
        if (!SystemLossUnderRecovery.Equals(systemLossUnderRecovery)) SystemLossUnderRecovery = systemLossUnderRecovery;
        if (!DistributionVAT.Equals(distributionVAT)) DistributionVAT = distributionVAT;
        if (!GenerationVAT.Equals(generationVAT)) GenerationVAT = generationVAT;
        if (!TransmissionVAT.Equals(transmissionVAT)) TransmissionVAT = transmissionVAT;
        if (!SystemLossVAT.Equals(systemLossVAT)) SystemLossVAT = systemLossVAT;
        if (!DifferentialBillPerKWH.Equals(differentialBillPerKWH)) DifferentialBillPerKWH = differentialBillPerKWH;
        if (!DifferentialBillCustomerPerMonth.Equals(differentialBillCustomerPerMonth)) DifferentialBillCustomerPerMonth = differentialBillCustomerPerMonth;
        if (!RFSCCAPEXCharge.Equals(rFSCCAPEXCharge)) RFSCCAPEXCharge = rFSCCAPEXCharge;
        if (!SeniorCitizenDiscount.Equals(seniorCitizenDiscount)) SeniorCitizenDiscount = seniorCitizenDiscount;
        if (!SeniorCitizenDiscountKWH.Equals(seniorCitizenDiscountKWH)) SeniorCitizenDiscountKWH = seniorCitizenDiscountKWH;
        if (!SeniorCitizenDiscountPercentage.Equals(seniorCitizenDiscountPercentage)) SeniorCitizenDiscountPercentage = seniorCitizenDiscountPercentage;
        if (!SeniorCitizenSubsidyKWH.Equals(seniorCitizenSubsidyKWH)) SeniorCitizenSubsidyKWH = seniorCitizenSubsidyKWH;
        if (!WheelingCharge.Equals(wheelingCharge)) WheelingCharge = wheelingCharge;
        if (!OtherGenerationCharge.Equals(otherGenerationCharge)) OtherGenerationCharge = otherGenerationCharge;
        if (!OtherTransmissionCharge.Equals(otherTransmissionCharge)) OtherTransmissionCharge = otherTransmissionCharge;
        if (!OtherTransmissionDemandCharge.Equals(otherTransmissionDemandCharge)) OtherTransmissionDemandCharge = otherTransmissionDemandCharge;
        if (!OtherSystemLossCharge.Equals(otherSystemLossCharge)) OtherSystemLossCharge = otherSystemLossCharge;
        if (!OtherLifelineCharge.Equals(otherLifelineCharge)) OtherLifelineCharge = otherLifelineCharge;
        if (!OtherSeniorCitizenCharge.Equals(otherSeniorCitizenCharge)) OtherSeniorCitizenCharge = otherSeniorCitizenCharge;
        if (!FeedInTariffAllowance.Equals(feedInTariffAllowance)) FeedInTariffAllowance = feedInTariffAllowance;
        if (!PowerActRecoveryCharge.Equals(powerActRecoveryCharge)) PowerActRecoveryCharge = powerActRecoveryCharge;
        if (!FinalVAT.Equals(finalVAT)) FinalVAT = finalVAT;
        if (!WithholdingTaxServices.Equals(withholdingTaxServices)) WithholdingTaxServices = withholdingTaxServices;
        if (!WithholdingTaxRental.Equals(withholdingTaxRental)) WithholdingTaxRental = withholdingTaxRental;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        return this;
    }
}