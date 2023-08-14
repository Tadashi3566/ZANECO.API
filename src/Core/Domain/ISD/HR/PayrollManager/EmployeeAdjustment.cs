using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Domain.ISD.HR.PayrollManager;

public class EmployeeAdjustment : AuditableEntity, IAggregateRoot
{
    public EmployeeAdjustment()
    {
    }

    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType EmployeeId { get; private set; }
    public string EmployeeName { get; private set; } = default!;
    public DefaultIdType AdjustmentId { get; private set; }
    public string AdjustmentType { get; private set; } = default!;
    public string AdjustmentName { get; private set; } = default!;
    public decimal Amount { get; private set; }
    public string PaymentSchedule { get; private set; } = default!;
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    public EmployeeAdjustment(DefaultIdType employeeId, string employeeName, DefaultIdType adjustmentId, string adjustmentType, string paymentSchedule, string adjustmentName, decimal amount, DateTime startDate, DateTime? endDate, string? description = null, string? notes = null, string? imagePath = null)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;

        AdjustmentId = adjustmentId;
        AdjustmentType = adjustmentType;
        PaymentSchedule = paymentSchedule;
        AdjustmentName = adjustmentName;
        Amount = amount;

        StartDate = startDate;
        EndDate = endDate;

        Name = string.Empty;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public EmployeeAdjustment Update(string employeeName, DefaultIdType? adjustmentId, string adjustmentType, string paymentSchedule, string adjustmentName, decimal amount, DateTime startDate, DateTime endDate, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (!EmployeeName.Equals(employeeName)) EmployeeName = employeeName;

        if (adjustmentId.HasValue && adjustmentId.Value != DefaultIdType.Empty && !AdjustmentId.Equals(adjustmentId.Value)) AdjustmentId = adjustmentId.Value;
        if (!AdjustmentType.Equals(adjustmentType)) AdjustmentType = adjustmentType;
        if (paymentSchedule is not null && !PaymentSchedule.Equals(paymentSchedule)) PaymentSchedule = paymentSchedule;

        if (adjustmentName is not null && !AdjustmentName.Equals(adjustmentName)) AdjustmentName = adjustmentName;
        if (!Amount.Equals(amount)) Amount = amount;

        if (!StartDate.Equals(startDate)) StartDate = startDate;
        if (!EndDate.Equals(endDate)) EndDate = endDate;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public EmployeeAdjustment ClearImagePath()
    {
        ImagePath = null;

        return this;
    }
}