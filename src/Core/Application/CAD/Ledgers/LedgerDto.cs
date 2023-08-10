namespace ZANECO.API.Application.CAD.Ledgers;

public class LedgerDto : BaseDto, IDto
{
    public DefaultIdType AccountId { get; set; } = default!;
    public double IdCode { get; set; } = default!;
    public string AccountNumber { get; set; } = default!;
    public string BillMonth { get; set; } = default!;
    public string BillNumber { get; set; } = default!;
    public double PresentReading { get; set; } = default!;
    public double KWH { get; set; } = default!;

    public decimal UCNPCSD { get; set; } = default!;
    public decimal UCNPCSCC { get; set; } = default!;
    public decimal UCDUSCC { get; set; } = default!;
    public decimal UCME { get; set; } = default!;
    public decimal UCETR { get; set; } = default!;
    public decimal UCEC { get; set; } = default!;
    public decimal UCCSR { get; set; } = default!;

    public decimal VATDist { get; set; } = default!;
    public decimal VATGen { get; set; } = default!;
    public decimal VATTrans { get; set; } = default!;
    public decimal VATSLGen { get; set; } = default!;
    public decimal VATSLTrans { get; set; } = default!;
    public decimal VAT { get; set; } = default!;

    public decimal Debit { get; set; } = default!;
    public decimal Credit { get; set; } = default!;
    public decimal Balance { get; set; } = default!;

    public string? Collector { get; set; }
    public DateTime PostingDate { get; set; } = default!;
}