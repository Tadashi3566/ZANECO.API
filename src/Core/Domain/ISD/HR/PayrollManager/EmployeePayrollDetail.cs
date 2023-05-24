using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Domain.ISD.HR.PayrollManager;

public class EmployeePayrollDetail : AuditableEntity, IAggregateRoot
{
    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType EmployeeId { get; private set; } = default!;
    public string EmployeeName { get; private set; } = default!;

    public virtual Payroll Payroll { get; private set; } = default!;
    public DefaultIdType PayrollId { get; private set; } = default!;
    public string PayrollName { get; private set; } = default!;

    public DefaultIdType AdjustmentId { get; private set; } = default!;
    public string AdjustmentType { get; private set; } = default!;
    public string AdjustmentName { get; private set; } = default!;

    public decimal Amount { get; private set; } = default!;
    public DateTime StartDate { get; private set; } = default!;
    public DateTime EndDate { get; private set; } = default!;
    public DateTime PayrollDate { get; private set; } = default!;

    public string Contributor { get; private set; }

    public EmployeePayrollDetail(DefaultIdType employeeId, string employeeName, DefaultIdType payrollId, string payrollName, DefaultIdType adjustmentId, string adjustmentType, string adjustmentName, decimal amount, DateTime startDate, DateTime endDate, DateTime payrollDate, string contributor, string? description = "", string? notes = "")
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;
        PayrollId = payrollId;
        PayrollName = payrollName;
        AdjustmentId = adjustmentId;
        AdjustmentType = adjustmentType;
        AdjustmentName = adjustmentName;

        Amount = amount;

        StartDate = startDate;
        EndDate = endDate;
        PayrollDate = payrollDate;

        Contributor = contributor;

        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();
    }

    public EmployeePayrollDetail Update(string employeeName, string payrollName, string adjustmentName, decimal amount, DateTime startDate, DateTime endDate, DateTime payrollDate, string contributor, string? description = "", string? notes = "")
    {
        if (!EmployeeName.Equals(employeeName)) EmployeeName = employeeName;
        if (!PayrollName.Equals(payrollName)) PayrollName = payrollName;

        if (!AdjustmentName.Equals(adjustmentName)) AdjustmentName = adjustmentName;

        if (!Amount.Equals(amount)) Amount = amount;

        if (!StartDate.Equals(startDate)) StartDate = startDate;
        if (!EndDate.Equals(endDate)) EndDate = endDate;
        if (!PayrollDate.Equals(payrollDate)) PayrollDate = payrollDate;

        if (!Contributor.Equals(contributor)) Contributor = contributor;

        if (description is not null && !Description!.Equals(description)) Description = description.Trim();
        if (notes is not null && !Notes!.Equals(notes)) Notes = notes.Trim();

        return this;
    }
}