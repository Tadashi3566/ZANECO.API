namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrolls;

public class EmployeePayrollDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType EmployeeId { get; set; } = default!;
    public string EmployeeName { get; set; } = default!;
    public DefaultIdType PayrollId { get; set; } = default!;
    public string PayrollName { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? PayrollDate { get; set; }
    public decimal Salary { get; set; } = default!;
    public decimal Additional { get; set; } = default!;
    public decimal Gross { get; set; } = default!;
    public decimal Deduction { get; set; } = default!;
    public decimal Net { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
}