namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrollDetails;

public class EmployeePayrollDetailDto : DtoExtension<EmployeePayrollDetailDto>, IDto
{
    public DefaultIdType EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public DefaultIdType PayrollId { get; set; }
    public string PayrollName { get; set; } = default!;
    public DefaultIdType AdjustmentId { get; set; }
    public string AdjustmentType { get; set; } = default!;
    public string AdjustmentName { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public DateTime PayrollDate { get; set; } = default!;
    public string Contributor { get; set; } = default!;
}