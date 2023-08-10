namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;

public class EmployeeAdjustmentExportDto : BaseDto, IDto
{
    public string EmployeeName { get; set; } = default!;
    public string AdjustmentType { get; set; } = default!;
    public int Number { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public bool IsMonthMid { get; set; }
    public bool IsMonthEnd { get; set; }
}