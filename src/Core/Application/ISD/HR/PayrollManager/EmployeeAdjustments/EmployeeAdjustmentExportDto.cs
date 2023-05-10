namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;

public class EmployeeAdjustmentExportDto : IDto
{
    public string EmployeeName { get; set; } = default!;
    public string AdjustmentType { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public bool IsMonthMid { get; set; }
    public bool IsMonthEnd { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
}