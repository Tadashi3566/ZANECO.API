﻿namespace ZANECO.API.Domain.CAD;

public class RemoteCollection : AuditableEntity, IAggregateRoot
{
    public double CollectorId { get; private set; } = default!;
    public string Collector { get; private set; } = default!;
    public string Reference { get; private set; } = default!;
    public DateTime TransactionDate { get; private set; } = default!;
    public DateTime ReportDate { get; private set; } = default!;
    public string AccountNumber { get; private set; } = default!;
    public decimal Amount { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? OrNumber { get; private set; }
    public string? ImagePath { get; private set; }

    public RemoteCollection(double collectorId, string collector, string reference, DateTime transactionDate, DateTime reportDate, string accountNumber, decimal amount, string name, string orNumber, string description, string notes, string? imagePath)
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

        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();

        ImagePath = imagePath;
    }

    public RemoteCollection Update(string orNumber, string description, string notes, string? imagePath)
    {
        if (orNumber is not null && !OrNumber!.Equals(orNumber)) OrNumber = orNumber;

        if (description is not null && !Description!.Equals(description)) Description = description.Trim();
        if (notes is not null && !Notes!.Equals(notes)) Notes = notes.Trim();

        if (!string.IsNullOrEmpty(imagePath) && !ImagePath!.Equals(imagePath)) ImagePath = imagePath;

        return this;
    }

    public RemoteCollection ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}