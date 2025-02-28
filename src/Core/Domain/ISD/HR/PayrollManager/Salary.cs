﻿namespace ZANECO.API.Domain.ISD.HR.PayrollManager;

public class Salary : AuditableEntity, IAggregateRoot
{
    public Salary()
    {
    }

    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string RateType { get; private set; } = default!;
    public int Number { get; private set; }

    public decimal Amount { get; private set; }
    public int IncrementYears { get; private set; }
    public decimal IncrementAmount { get; private set; }
    public bool IsActive { get; private set; }

    public Salary(DateTime startDate, DateTime endDate, string rateType, int number, string name, decimal amount, int incrementYears, decimal incrementAmount, bool isActive, string? description = null, string? notes = null)
    {
        StartDate = startDate;
        EndDate = endDate;

        RateType = rateType;
        Number = number;
        Name = name.Trim().ToUpper();
        Amount = amount;
        IncrementYears = incrementYears;
        IncrementAmount = incrementAmount;
        IsActive = isActive;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
    }

    public Salary Update(DateTime startDate, DateTime endDate, string rateType, int number, string name, decimal amount, int incrementYears, decimal incrementAmount, bool isActive, string? description = null, string? notes = null)
    {
        if (!StartDate.Equals(startDate)) StartDate = startDate;
        if (!EndDate.Equals(endDate)) EndDate = endDate;

        if (!RateType.Equals(rateType)) RateType = rateType;
        if (!Number.Equals(number)) Number = number;
        if (!Name.Equals(name)) Name = name.Trim().ToUpper();
        if (!Amount.Equals(amount)) Amount = amount;
        if (!IncrementYears.Equals(incrementYears)) IncrementYears = incrementYears;
        if (!IncrementAmount.Equals(incrementAmount)) IncrementAmount = incrementAmount;
        if (!IsActive.Equals(isActive)) IsActive = isActive;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        return this;
    }
}