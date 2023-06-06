namespace ZANECO.API.Domain.Catalog;

public class Sale : AuditableEntity, IAggregateRoot
{
    public Sale()
    {
    }

    public virtual Customer Customer { get; private set; } = default!;
    public DefaultIdType CustomerId { get; private set; }
    public DateTime Date { get; private set; } = default!;
    public string Transaction { get; private set; } = default!;
    public double OrNumber { get; private set; } = default!;
    public int Items { get; private set; } = default!;
    public decimal GrossSales { get; private set; } = default!;
    public decimal TotalVat { get; private set; } = default!;
    public decimal TotalDiscount { get; private set; } = default!;
    public decimal NetSales { get; private set; } = default!;
    public decimal Received { get; private set; } = default!;
    public decimal Change { get; private set; } = default!;
    public string? ImagePath { get; private set; }

    public Sale(DefaultIdType customerId, double orNumber, int items, decimal grossSales, decimal totalVat, decimal totalDiscount, decimal netSales, decimal received, decimal change, string? description, string? notes, string? imagePath)
    {
        var dt = DateTime.Now;

        CustomerId = customerId;

        Date = dt;
        Transaction = $"SALE{dt:yyyyMMddHHmmsss}";
        OrNumber = orNumber;
        Items = items;

        GrossSales = grossSales;
        TotalVat = totalVat;
        TotalDiscount = totalDiscount;
        NetSales = netSales;
        Received = received;
        Change = change;

        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();

        ImagePath = imagePath;
    }

    public Sale Update(DefaultIdType customerId, double orNumber, int items, decimal grossSales, decimal totalVat, decimal totalDiscount, decimal netSales, decimal received, decimal change, string? description, string? notes, string? imagePath)
    {
        if (!customerId.Equals(DefaultIdType.Empty) && !CustomerId.Equals(customerId)) CustomerId = customerId;

        if (!OrNumber.Equals(orNumber)) OrNumber = orNumber;
        if (!Items.Equals(items)) Items = items;

        if (!GrossSales.Equals(grossSales)) GrossSales = grossSales;
        if (!TotalVat.Equals(totalVat)) TotalVat = totalVat;
        if (!TotalDiscount.Equals(totalDiscount)) TotalDiscount = totalDiscount;
        if (!NetSales.Equals(netSales)) NetSales = netSales;
        if (!Received.Equals(received)) Received = received;
        if (!Change.Equals(change)) Change = change;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        if (!string.IsNullOrEmpty(imagePath) && !ImagePath!.Equals(imagePath)) ImagePath = imagePath;

        return this;
    }

    public Sale ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}