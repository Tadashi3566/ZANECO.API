using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Domain.ISD.HR.PayrollManager;

public class Loan : AuditableEntityWithApproval<DefaultIdType>, IAggregateRoot
{
    public Loan()
    {
    }

    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType EmployeeId { get; private set; } = default!;
    public string EmployeeName { get; private set; } = default!;

    public virtual Adjustment Adjustment { get; private set; } = default!;
    public DefaultIdType AdjustmentId { get; private set; } = default!;
    public string AdjustmentName { get; private set; } = default!;

    public decimal Amount { get; private set; } = default!;
    public DateTime DateReleased { get; private set; } = default!;
    public string PaymentSchedule { get; private set; } = default!;
    public int Months { get; private set; } = default!;
    public decimal Ammortization { get; private set; } = default!;

    public DateTime StartDate { get; private set; } = default!;
    public DateTime EndDate { get; private set; } = default!;
    public string? ImagePath { get; private set; }

    public Loan(DefaultIdType employeeId, string employeeName, DefaultIdType adjustmentId, string adjustmentName, decimal amount, DateTime dateReleased, string paymentSchedule, int months, decimal ammortization, DateTime startDate, DateTime endDate, string? description, string? notes, string? imagePath)
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

        Status = "ON-GOING";
        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();

        ImagePath = imagePath;
    }

    public Loan Update(string employeeName, DefaultIdType adjustmentId, string adjustmentName, decimal amount, DateTime dateReleased, string paymentSchedule, int months, decimal ammortization, DateTime startDate, DateTime endDate, string? description, string? notes, string? imagePath)
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

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        if (!string.IsNullOrEmpty(imagePath) && !ImagePath!.Equals(imagePath)) ImagePath = imagePath;

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