namespace ZANECO.API.Application.CAD.Accounts;

public class AccountDto : DtoExtension, IDto
{
    public int IdCode { get; set; } = default!;
    public string AccountNumber { get; set; } = default!;
    public string Area { get; set; } = default!;
    public string Route { get; set; } = default!;
    public string Cipher { get; set; } = default!;
    public string Tag { get; set; } = default!;
    public string Address { get; set; } = default!;

    public string AccountType { get; set; } = default!; // Residential, High and Low Voltage
    public string Feeder { get; set; } = default!;
    public string Pole { get; set; } = default!;
    public string Transformer { get; set; } = default!;
    public string MeterBrand { get; set; } = default!;
    public string MeterSerial { get; set; } = default!;

    public string ConnectionStatus { get; set; } = default!; // Active, Disconnected etc.

    public DateTime? ConnectionDate { get; set; }
    public DateTime? DisconnectionDate { get; set; }
    public DateTime? ReconnectionDate { get; set; }

    public string BillMonth { get; set; } = default!;
    public DateTime PreviousReadingDate { get; set; } = default!;
    public decimal PreviousReadingKWH { get; set; } = default!;
    public DateTime PresentReadingDate { get; set; } = default!;
    public decimal PresentReadingKWH { get; set; } = default!;
    public decimal UsedKWH { get; set; } = default!;
    public int Multiplier { get; set; }
    public decimal BillAmount { get; set; } = default!;
    public string BillNumber { get; set; } = default!;

    public bool ChangedMeter { get; set; }
    public decimal PreviousReadingKWHCM { get; set; }
    public decimal PresentReadingKWHCM { get; set; }
    public decimal UsedKWHCM { get; set; } = default!;


}