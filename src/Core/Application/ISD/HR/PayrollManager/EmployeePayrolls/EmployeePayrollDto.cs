namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrolls;

public class EmployeePayrollDto : DtoExtension, IDto
{
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
}