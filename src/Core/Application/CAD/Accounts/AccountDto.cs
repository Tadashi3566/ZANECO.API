namespace ZANECO.API.Application.CAD.Accounts;

public class AccountDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int IdCode { get; set; } = default!;
    public string AccountNumber { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string Route { get; set; } = string.Empty;
    public string Cipher { get; set; } = string.Empty;
    public string Tag { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public string AccountType { get; set; } = string.Empty; // Residential, High and Low Voltage
    public string Feeder { get; set; } = string.Empty;
    public string Pole { get; set; } = string.Empty;
    public string Transformer { get; set; } = string.Empty;
    public string MeterBrand { get; set; } = string.Empty;
    public string MeterSerial { get; set; } = string.Empty;

    public string ConnectionStatus { get; set; } = string.Empty; // Active, Disconnected etc.

    public DateTime? ConnectionDate { get; set; }
    public DateTime? DisconnectionDate { get; set; }
    public DateTime? ReconnectionDate { get; set; }

    public string BillMonth { get; set; } = string.Empty;
    public DateTime PreviousReadingDate { get; set; } = default!;
    public decimal PreviousReadingKWH { get; set; } = default!;
    public DateTime PresentReadingDate { get; set; } = default!;
    public decimal PresentReadingKWH { get; set; } = default!;
    public decimal UsedKWH { get; set; } = default!;
    public int Multiplier { get; set; }
    public decimal BillAmount { get; set; } = default!;
    public string BillNumber { get; set; } = string.Empty;

    public bool ChangedMeter { get; set; }
    public decimal PreviousReadingKWHCM { get; set; }
    public decimal PresentReadingKWHCM { get; set; }
    public decimal UsedKWHCM { get; set; } = default!;

    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
}