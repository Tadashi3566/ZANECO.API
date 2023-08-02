using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Domain.ISD.HR.PayrollManager;

public class Loan : AuditableEntityWithApproval<DefaultIdType>, IAggregateRoot
{
    public Loan()
    {
    }

    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType EmployeeId { get; private set; }
    public string EmployeeName { get; private set; } = default!;

    public virtual Adjustment Adjustment { get; private set; } = default!;
    public DefaultIdType AdjustmentId { get; private set; }
    public string AdjustmentName { get; private set; } = default!;

    public decimal Amount { get; private set; }
    public DateTime DateReleased { get; private set; }
    public string PaymentSchedule { get; private set; } = default!;
    public int Months { get; private set; }
    public decimal Ammortization { get; private set; }

    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string? ImagePath { get; private set; }

    public Loan(DefaultIdType employeeId, string employeeName, DefaultIdType adjustmentId, string adjustmentName, decimal amount, DateTime dateReleased, string paymentSchedule, int months, decimal ammortization, DateTime startDate, DateTime endDate, string? description = null, string? notes = null, string? imagePath = null)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;

        AdjustmentId = adjustmentId;
        AdjustmentName = adjustmentName;

        DateReleased = dateReleased;
        Amount = amount;
        PaymentSchedule = paymentSchedule;
        Months = months;
        Ammortization = ammortization;

        StartDate = startDate;
        EndDate = endDate;

        Name = string.Empty;
        Status = "ON-GOING";

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public Loan Update(string employeeName, DefaultIdType adjustmentId, string adjustmentName, decimal amount, DateTime dateReleased, string paymentSchedule, int months, decimal ammortization, DateTime startDate, DateTime endDate, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (employeeName is not null && !EmployeeName.Equals(employeeName)) EmployeeName = employeeName;

        if (!adjustmentId.Equals(DefaultIdType.Empty) && !AdjustmentId.Equals(adjustmentId)) AdjustmentId = adjustmentId;
        if (adjustmentName is not null && !AdjustmentName.Equals(adjustmentName)) AdjustmentName = adjustmentName;

        if (dateReleased != default && !DateReleased.Equals(dateReleased)) DateReleased = dateReleased;
        if (!Amount.Equals(amount)) Amount = amount;
        if (paymentSchedule is not null && !PaymentSchedule.Equals(paymentSchedule)) PaymentSchedule = paymentSchedule;
        if (!Months.Equals(months)) Months = months;
        if (!Ammortization.Equals(ammortization)) Ammortization = ammortization;

        if (!StartDate.Equals(startDate)) StartDate = startDate;
        if (!EndDate.Equals(endDate)) EndDate = endDate;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public Loan Close(string status)
    {
        if (status is not null && !Status!.Equals(status)) Status = status;

        return this;
    }

    public Loan ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}