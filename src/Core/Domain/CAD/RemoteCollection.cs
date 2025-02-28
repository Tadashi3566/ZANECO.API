﻿namespace ZANECO.API.Domain.CAD;

public class RemoteCollection : AuditableEntity, IAggregateRoot
{
    public double CollectorId { get; private set; }
    public string Collector { get; private set; } = default!;
    public string Reference { get; private set; } = default!;
    public DateTime TransactionDate { get; private set; }
    public DateTime ReportDate { get; private set; }
    public string AccountNumber { get; private set; } = default!;
    public decimal Amount { get; private set; }

    public string? OrNumber { get; private set; }

    public RemoteCollection(double collectorId, string collector, string reference, DateTime transactionDate, DateTime reportDate, string accountNumber, decimal amount, string name, string orNumber, string? description = null, string? notes = null, string? imagePath = null)
    {
        CollectorId = collectorId;
        Collector = collector;
        Reference = reference;

        TransactionDate = transactionDate;
        ReportDate = reportDate;

        AccountNumber = accountNumber;
        Amount = amount;
        Name = name.Trim().ToUpper();
        OrNumber = orNumber;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public RemoteCollection Update(string orNumber, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (orNumber is not null && !OrNumber!.Equals(orNumber)) OrNumber = orNumber;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public RemoteCollection ClearImagePath()
    {
        ImagePath = null;

        return this;
    }
}