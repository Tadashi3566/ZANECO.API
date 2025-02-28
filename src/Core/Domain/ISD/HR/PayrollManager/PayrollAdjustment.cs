﻿namespace ZANECO.API.Domain.ISD.HR.PayrollManager;

public class PayrollAdjustment : AuditableEntity, IAggregateRoot
{
    public virtual Payroll Payroll { get; private set; } = default!;
    public DefaultIdType PayrollId { get; private set; }
    public string PayrollName { get; private set; } = default!;
    public virtual Adjustment Adjustment { get; private set; } = default!;
    public DefaultIdType AdjustmentId { get; private set; }
    public int AdjustmentNumber { get; private set; }
    public string AdjustmentName { get; private set; } = default!;

    public PayrollAdjustment(DefaultIdType payrollId, string payrollName, DefaultIdType adjustmentId, int adjustmentNumber, string adjustmentName, string? description = null, string? notes = null)
    {
        PayrollId = payrollId;
        PayrollName = payrollName;

        AdjustmentId = adjustmentId;
        AdjustmentNumber = adjustmentNumber;
        AdjustmentName = adjustmentName;

        Name = string.Empty;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
    }

    public PayrollAdjustment Update(string payrollName, int adjustmentNumber, string adjustmentName, string? description = null, string? notes = null)
    {
        if (payrollName is not null && !PayrollName.Equals(payrollName)) PayrollName = payrollName;
        if (!AdjustmentNumber.Equals(adjustmentNumber)) AdjustmentNumber = adjustmentNumber;
        if (adjustmentName is not null && !AdjustmentName.Equals(adjustmentName)) AdjustmentName = adjustmentName;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        return this;
    }
}