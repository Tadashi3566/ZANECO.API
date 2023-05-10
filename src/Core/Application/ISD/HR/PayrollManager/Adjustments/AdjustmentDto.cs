namespace ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;

public class AdjustmentDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string AdjustmentType { get; set; } = string.Empty;
    public string EmployeeType { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public string PaymentSchedule { get; set; } = default!;
    public bool IsOptional { get; set; }
    public bool IsLoan { get; set; }
    public bool IsActive { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
}