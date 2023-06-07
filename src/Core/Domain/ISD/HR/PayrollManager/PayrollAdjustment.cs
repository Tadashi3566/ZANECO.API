namespace ZANECO.API.Domain.ISD.HR.PayrollManager;

public class PayrollAdjustment : AuditableEntity, IAggregateRoot
{
    public virtual Payroll Payroll { get; private set; } = default!;
    public DefaultIdType PayrollId { get; private set; } = default!;
    public string PayrollName { get; private set; } = default!;
    public virtual Adjustment Adjustment { get; private set; } = default!;
    public DefaultIdType AdjustmentId { get; private set; } = default!;
    public int AdjustmentNumber { get; private set; } = default!;
    public string AdjustmentName { get; private set; } = default!;

    public PayrollAdjustment(DefaultIdType payrollId, string payrollName, DefaultIdType adjustmentId, int adjustmentNumber, string adjustmentName, string? description = "", string? notes = "")
    {
        PayrollId = payrollId;
        PayrollName = payrollName;

        AdjustmentId = adjustmentId;
        AdjustmentNumber = adjustmentNumber;
        AdjustmentName = adjustmentName;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
    }

    public PayrollAdjustment Update(string payrollName, int adjustmentNumber, string adjustmentName, string? description = "", string? notes = "")
    {
        if (payrollName is not null && !PayrollName.Equals(payrollName)) PayrollName = payrollName;
        if (!AdjustmentNumber.Equals(adjustmentNumber)) AdjustmentNumber = adjustmentNumber;
        if (adjustmentName is not null && !AdjustmentName.Equals(adjustmentName)) AdjustmentName = adjustmentName;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        return this;
    }
}