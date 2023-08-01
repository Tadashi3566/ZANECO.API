namespace ZANECO.API.Domain.CAD;

public class Master : AuditableEntity<int>, IAggregateRoot
{
    public int Code { get; set; }
    public string? AccountNumber { get; set; }
    public string? Address { get; set; }
    public string? Area { get; set; }
    public string? Book { get; set; }
    public string? CBook { get; set; }
    public string? Sequence { get; set; }

    public string? WRateCode { get; set; }
    public string? FeederNumber { get; set; }
    public string? PoleNumber { get; set; }
    public string? Transformer { get; set; }
    public string? MeterBrand { get; set; }
    public string? Serial { get; set; }

    public DateTime NewConnectionDate { get; set; } = DateTime.Today;
    public DateTime DisconnectionDate { get; set; } = DateTime.Today;
    public DateTime ReconnectionDate { get; set; } = DateTime.Today;

    public string? BillMonth { get; set; }
    public DateTime PreviousReadingDate { get; set; } = DateTime.Today;
    public double PreviousReadingKWH { get; set; }
    public DateTime PresentReadingDate { get; set; } = DateTime.Today;
    public double PresentReadingKWH { get; set; }
    public double KilowattHour { get; set; }
    public decimal TotalBill { get; set; }
    public string? OEBRNumber { get; set; }
}