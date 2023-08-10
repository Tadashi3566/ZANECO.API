namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;

public class EmployeeAdjustmentDto : DtoExtension, IDto
{
    public DefaultIdType EmployeeId { get; set; } = default!;
    public string EmployeeName { get; set; } = default!;
    public DefaultIdType AdjustmentId { get; set; } = default!;
    public string AdjustmentType { get; set; } = default!;
    public string PaymentSchedule { get; set; } = default!;
    public string AdjustmentName { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public DateTime StartDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
}