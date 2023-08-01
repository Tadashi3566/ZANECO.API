namespace ZANECO.API.Domain.CAD;

public class Billing : AuditableEntity, IAggregateRoot
{
    public Billing()
    {
    }

    public virtual Account Account { get; private set; } = default!;
    public DefaultIdType AccountId { get; private set; }
    public int IdCode { get; private set; }
    public string AccountNumber { get; private set; } = default!;
    public string BillMonth { get; private set; } = default!;
    public string BillNumber { get; private set; } = default!;
    public string CosumerType { get; private set; } = default!;
    public int Multiplier { get; private set; } = default!;
    public string MeterSerial { get; private set; } = default!;

    public decimal Total { get; private set; } = default!;
    public decimal Generation { get; private set; } = default!;
    public decimal Transmission { get; private set; } = default!;
    public decimal TransmissionDemand { get; private set; } = default!;
    public decimal Distribution { get; private set; } = default!;
    public decimal DistributionDemand { get; private set; } = default!;
    public decimal HostComm { get; private set; } = default!;
    public decimal Forex { get; private set; } = default!;
    public decimal Supply { get; private set; } = default!;
    public decimal SupplyRetailCustomer { get; private set; } = default!;
    public decimal Metering { get; private set; } = default!;
    public decimal MeteringRetail { get; private set; } = default!;
    public decimal SystemLoss { get; private set; } = default!;

    public decimal UniChargeStrandedDebt { get; private set; } = default!;
    public decimal UniChargeStrandedContractNPC { get; private set; } = default!;
    public decimal UniChargeStrandedContractDU { get; private set; } = default!;
    public decimal UniChargeMissionaryElectrification { get; private set; } = default!;
    public decimal UniChargeEqalTaxRoyalties { get; private set; } = default!;
    public decimal UniChargeEnvironmental { get; private set; } = default!;
    public decimal UniChargeCrossSubsidyRemoval { get; private set; } = default!;

    public decimal InterClassCrossSubsidy { get; private set; } = default!;
    public decimal PowerActReduction { get; private set; } = default!;
    public decimal PowerActRecovery { get; private set; } = default!;
    public decimal FeedInTariffAllowance { get; private set; } = default!;
    public decimal LifelineSubsidy { get; private set; } = default!;
    public decimal LoanCondonation { get; private set; } = default!;
    public decimal Gram { get; private set; } = default!;
    public decimal Capex { get; private set; } = default!;

    public decimal OtherGenerationAdjustment { get; private set; } = default!;
    public decimal OtherTransmissionAdjustment { get; private set; } = default!;
    public decimal OtherTransmissionDemandAdjustment { get; private set; } = default!;
    public decimal OtherSystemLossAdjustment { get; private set; } = default!;
    public decimal OtherLifelineAdjustment { get; private set; } = default!;
    public decimal OtherSeniorCitizenAdjustment { get; private set; } = default!;
    public decimal SystemLossUnderRecovery { get; private set; } = default!;

    public decimal VAT { get; private set; }
    public decimal VATGeneration { get; private set; }
    public decimal VATTransmission { get; private set; }
    public decimal VATDistribution { get; private set; }
    public decimal VATSLGeneration { get; private set; }
    public decimal VATSLTransmission { get; private set; }

    public decimal VATPowerActRecovery { get; private set; } = default!;
    public decimal VATCapex { get; private set; } = default!;
    public decimal VATDiscount { get; private set; } = default!;
    public decimal VATDiscountAmount { get; private set; } = default!;

    public Billing(string accountNumber, string? description = null, string? notes = null)
    {
        AccountNumber = accountNumber.Trim().ToUpper();
        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
    }

    public Billing(DefaultIdType accountId, int idCode, string accountNumber, string billNumber, string billMonth, decimal ucnpcsd, decimal ucnpcscc, decimal ucduscc, decimal ucme, decimal ucetr, decimal ucec, decimal uccsr, decimal vatDistribution, decimal vatGeneration, decimal vatTransmission, decimal vatSLGeneration, decimal vatSLTransmission, decimal vat)
    {
        AccountId = accountId;
        IdCode = idCode;
        AccountNumber = accountNumber.Trim().ToUpper();
        BillNumber = billNumber;
        BillMonth = billMonth;

        //UCNPCSD = ucnpcsd;
        //UCNPCSCC = ucnpcscc;
        //UCDUSCC = ucduscc;
        //UCME = ucme;
        //UCETR = ucetr;
        //UCEC = ucec;
        //UCCSR = uccsr;

        VATDistribution = vatDistribution;
        VATGeneration = vatGeneration;
        VATTransmission = vatTransmission;
        VATSLGeneration = vatSLGeneration;
        VATSLTransmission = vatSLTransmission;
        VAT = vat;

        //if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        //if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
    }

    public Billing Update(string accountNumber, string? description = null, string? notes = null)
    {
        if (accountNumber is not null && !AccountNumber.Equals(AccountNumber)) AccountNumber = accountNumber.Trim().ToUpper();
        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
        return this;
    }

    // public Billing Update(string accountNumber, string document, string billMonth, float reading, float kwh, decimal ucnpcsd, decimal ucnpcscc, decimal ucduscc, decimal ucme, decimal ucetr, decimal ucec, decimal uccsr, decimal vatDist, decimal vatGen, decimal vatTrans, decimal vatSLGen, decimal vatSLTrans, decimal vat, decimal vatDiscount, decimal vatDistDiscoun, decimal vatGenDiscount, decimal vatTransDiscount, decimal VATSLGenDiscount, decimal VATSLTransDiscount, string collector, DateTime? postingDate, string? description = null, string? notes = null)
    // {
    //    if (accountNumber is not null && Reference?.Equals(accountNumber) is not true) Reference = accountNumber.Trim().ToUpper();
    //    if (document is not null && Documents?.Equals(document) is not true) Documents = document.Trim().ToUpper();
    //    if (billMonth is not null && BillMonth?.Equals(billMonth) is not true) BillMonth = billMonth;
    //    if (reading > 0 && !Reading.Equals(reading)) Reading = reading;
    //    if (kwh > 0 && !KWH.Equals(kwh)) KWH = kwh;
    //    if (collector is not null && Collector?.Equals(collector) is not true) Collector = collector.Trim().ToUpper();
    //    if (postingDate is not null && PostingDate?.Equals(postingDate) is not true) PostingDate = postingDate;
    //    if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
    //    if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
    //    return this;
    // }
}