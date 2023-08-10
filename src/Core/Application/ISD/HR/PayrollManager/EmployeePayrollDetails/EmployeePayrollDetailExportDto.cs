namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrollDetails;

public class EmployeePayrollDetailExportDto : BaseDto, IDto
{
    public string EmployeeName { get; set; } = default!;
    public string PayrollName { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public DateTime PayrollDate { get; set; } = default!;
}