﻿namespace ZANECO.API.Domain.CAD;

public class Account : AuditableEntity, IAggregateRoot
{
    public Account()
    {
    }

    public string IdCode { get; private set; } = default!;
    public string AccountNumber { get; private set; } = default!;

    public string Address { get; private set; } = default!;
    public string Area { get; private set; } = default!;
    public string Route { get; private set; } = default!;
    public string Cipher { get; private set; } = default!;
    public string Tag { get; private set; } = default!;

    public string AccountType { get; private set; } = default!; // Residential, High and Low Voltage
    public string Feeder { get; private set; } = default!;
    public string Pole { get; private set; } = default!;
    public string Transformer { get; private set; } = default!;
    public string MeterBrand { get; private set; } = default!;
    public string MeterSerial { get; private set; } = default!;

    public string ConnectionStatus { get; private set; } = default!; // Active, Disconnected etc.

    public DateTime? ConnectionDate { get; private set; } = DateTime.MinValue;
    public DateTime? DisconnectionDate { get; private set; } = DateTime.MinValue;
    public DateTime? ReconnectionDate { get; private set; } = DateTime.MinValue;

    public string BillMonth { get; private set; } = default!;
    public DateTime? PreviousReadingDate { get; private set; } = DateTime.MinValue;
    public double PreviousReadingKWH { get; private set; }
    public DateTime? PresentReadingDate { get; private set; } = DateTime.MinValue;
    public double PresentReadingKWH { get; private set; }
    public double UsedKWH { get; private set; }
    public int Multiplier { get; private set; }
    public decimal BillAmount { get; private set; }
    public string BillNumber { get; private set; } = default!;

    public bool ChangedMeter { get; private set; }
    public double PreviousReadingKWHCM { get; private set; }
    public double PresentReadingKWHCM { get; private set; }
    public double UsedKWHCM { get; private set; } = default!;

    public Account(string idCode, string accountNumber, string area, string route, string cipher, string tag, string name, string address, string accountType, string feeder, string pole, string transformer, string meterBrand, string meterSerial, string billMonth, DateTime previousReadingDate, double previousReadingKWH, DateTime presentReadingDate, double presentReadingKWH, double usedKWH, decimal billAmount, string? description = null, string? notes = null, string? imagePath = null)
    {
        IdCode = idCode;
        AccountNumber = accountNumber;
        Area = area;
        Route = route;
        Cipher = cipher;
        Tag = tag;

        Name = name.Trim().ToUpper();
        Address = address.Trim().ToUpper();

        AccountType = accountType;
        Feeder = feeder;
        Pole = pole;
        Transformer = transformer;
        MeterBrand = meterBrand;
        MeterSerial = meterSerial;

        BillMonth = billMonth;
        PreviousReadingDate = previousReadingDate;
        PreviousReadingKWH = previousReadingKWH;
        PresentReadingDate = presentReadingDate;
        PresentReadingKWH = presentReadingKWH;
        UsedKWH = usedKWH;
        BillAmount = billAmount;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public Account Update(string area, string route, string cipher, string tag, string name, string address, string accountType, string feeder, string pole, string transformer, string meterBrand, string meterSerial, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (area is not null && !Area.Equals(area)) Area = area.Trim();
        if (route is not null && !Route.Equals(route)) Route = route.Trim();
        if (cipher is not null && !Cipher.Equals(cipher)) Cipher = cipher.Trim();
        if (tag is not null && !Tag.Equals(tag)) Tag = tag.Trim();

        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();
        if (address is not null && !Address.Equals(address)) Address = address.Trim().ToUpper();

        if (accountType is not null && !AccountType.Equals(accountType)) AccountType = accountType.Trim().ToUpper();
        if (feeder is not null && !Feeder.Equals(feeder)) Feeder = feeder.Trim().ToUpper();
        if (pole is not null && !Pole.Equals(pole)) Pole = pole.Trim().ToUpper();
        if (transformer is not null && !Transformer.Equals(transformer)) Transformer = transformer.Trim().ToUpper();
        if (meterBrand is not null && !MeterBrand.Equals(meterBrand)) MeterBrand = meterBrand.Trim().ToUpper();
        if (meterSerial is not null && !MeterSerial.Equals(address)) MeterSerial = meterSerial.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public Account Migrate(string connectionStatus, DateTime connectionDate, DateTime disconnectionDate, DateTime reconnectionDate, string billMonth, DateTime previousReadingDate, double previousReadingKWH, DateTime? presentReadingDate, double presentReadingKWH, double usedKWH, decimal billAmount)
    {
        if (connectionStatus is not null && !ConnectionStatus.Equals(connectionStatus)) ConnectionStatus = connectionStatus.Trim().ToUpper();

        if (!ConnectionDate.Equals(connectionDate)) ConnectionDate = connectionDate;
        if (!DisconnectionDate.Equals(disconnectionDate)) DisconnectionDate = disconnectionDate;
        if (!ReconnectionDate.Equals(reconnectionDate)) ReconnectionDate = reconnectionDate;

        if (!BillMonth.Equals(billMonth)) BillMonth = billMonth.Trim();
        if (!PreviousReadingDate.Equals(previousReadingDate)) PreviousReadingDate = previousReadingDate;
        if (!PreviousReadingKWH.Equals(previousReadingKWH)) PreviousReadingKWH = previousReadingKWH;
        if (!PresentReadingDate.Equals(presentReadingDate)) PresentReadingDate = presentReadingDate;
        if (!PresentReadingKWH.Equals(presentReadingKWH)) PresentReadingKWH = presentReadingKWH;
        if (!UsedKWH.Equals(usedKWH)) UsedKWH = usedKWH;
        if (!BillAmount.Equals(billAmount)) BillAmount = billAmount;

        return this;
    }

    public Account ClearImagePath()
    {
        ImagePath = null;
        return this;
    }
}