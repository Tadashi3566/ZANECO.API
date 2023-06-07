namespace ZANECO.API.Domain.CAD;

public class Ledger : AuditableEntity, IAggregateRoot
{
    public Ledger()
    {
    }

    public virtual Account Account { get; private set; } = default!;
    public DefaultIdType AccountId { get; private set; } = default!;
    public int IdCode { get; private set; } = default!;
    public string AccountNumber { get; private set; } = default!;
    public string BillMonth { get; private set; } = default!;
    public string BillNumber { get; private set; } = default!;
    public double LastReading { get; private set; } = default!;
    public double KWH { get; private set; } = default!;

    public decimal UCNPCSD { get; private set; } = default!;
    public decimal UCNPCSCC { get; private set; } = default!;
    public decimal UCDUSCC { get; private set; } = default!;
    public decimal UCME { get; private set; } = default!;
    public decimal UCETR { get; private set; } = default!;
    public decimal UCEC { get; private set; } = default!;
    public decimal UCCSR { get; private set; } = default!;

    public decimal VATDistribution { get; private set; } = default!;
    public decimal VATGeneration { get; private set; } = default!;
    public decimal VATTransmission { get; private set; } = default!;
    public decimal VATSLGeneration { get; private set; } = default!;
    public decimal VATSLTransmission { get; private set; } = default!;
    public decimal VAT { get; private set; } = default!;
    public decimal VATDiscount { get; private set; } = default!;

    public decimal Debit { get; private set; } = default!;
    public decimal Credit { get; private set; } = default!;
    public decimal Balance { get; private set; } = default!;

    public string? Collector { get; private set; } = string.Empty;
    public DateTime PostingDate { get; private set; }

    public Ledger(string accountNumber, string? description = "", string? notes = "")
    {
        AccountNumber = accountNumber.Trim().ToUpper();
        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
    }

    public Ledger(DefaultIdType accountId, int idCode, string accountNumber, string billNumber, string billMonth, double lastReading, double kwh, decimal ucnpcsd, decimal ucnpcscc, decimal ucduscc, decimal ucme, decimal ucetr, decimal ucec, decimal uccsr, decimal vatDistribution, decimal vatGeneration, decimal vatTransmission, decimal vatSLGeneration, decimal vatSLTransmission, decimal vat, decimal vatDiscount, decimal debit, decimal credit, decimal balance, string collector, DateTime postingDate)
    {
        AccountId = accountId;
        IdCode = idCode;
        AccountNumber = accountNumber.Trim().ToUpper();
        BillNumber = billNumber;
        BillMonth = billMonth;
        LastReading = lastReading;
        KWH = kwh;

        UCNPCSD = ucnpcsd;
        UCNPCSCC = ucnpcscc;
        UCDUSCC = ucduscc;
        UCME = ucme;
        UCETR = ucetr;
        UCEC = ucec;
        UCCSR = uccsr;

        VATDistribution = vatDistribution;
        VATGeneration = vatGeneration;
        VATTransmission = vatTransmission;
        VATSLGeneration = vatSLGeneration;
        VATSLTransmission = vatSLTransmission;
        VAT = vat;
        VATDiscount = vatDiscount;

        Debit = debit;
        Credit = credit;
        Balance = balance;

        Collector = collector.Trim().ToUpper();
        PostingDate = postingDate;

        //if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        //if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
    }

    public Ledger Update(string accountNumber, string? description = "", string? notes = "")
    {
        if (accountNumber is not null && !AccountNumber.Equals(AccountNumber)) AccountNumber = accountNumber.Trim().ToUpper();
        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
        return this;
    }

    // public Ledger Update(string accountNumber, string document, string billMonth, float reading, float kwh, decimal ucnpcsd, decimal ucnpcscc, decimal ucduscc, decimal ucme, decimal ucetr, decimal ucec, decimal uccsr, decimal vatDist, decimal vatGen, decimal vatTrans, decimal vatSLGen, decimal vatSLTrans, decimal vat, decimal vatDiscount, decimal vatDistDiscoun, decimal vatGenDiscount, decimal vatTransDiscount, decimal VATSLGenDiscount, decimal VATSLTransDiscount, string collector, DateTime? postingDate, string? description = "", string? notes = "")
    // {
    //    if (accountNumber is not null && Reference?.Equals(accountNumber) is not true) Reference = accountNumber.Trim().ToUpper();
    //    if (document is not null && Documents?.Equals(document) is not true) Documents = document.Trim().ToUpper();
    //    if (billMonth is not null && BillMonth?.Equals(billMonth) is not true) BillMonth = billMonth;
    //    if (reading > 0 && !Reading.Equals(reading)) Reading = reading;
    //    if (kwh > 0 && !KWH.Equals(kwh)) KWH = kwh;
    //    if (collector is not null && Collector?.Equals(collector) is not true) Collector = collector.Trim().ToUpper();
    //    if (postingDate is not null && PostingDate?.Equals(postingDate) is not true) PostingDate = postingDate;
    //    if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
    //    if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
    //    return this;
    // }
}