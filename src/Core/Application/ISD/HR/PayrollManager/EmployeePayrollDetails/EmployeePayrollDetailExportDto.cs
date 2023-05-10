namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrollDetails;

public class EmployeePayrollDetailExportDto : IDto
{
    public string EmployeeName { get; set; } = default!;
    public string PayrollName { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    public DateTime PayrollDate { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
}