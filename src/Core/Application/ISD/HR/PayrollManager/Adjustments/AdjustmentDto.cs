namespace ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;

public class AdjustmentDto : DtoExtension<AdjustmentDto>, IDto
{
    public string AdjustmentType { get; set; } = default!;
    public string EmployeeType { get; set; } = default!;
    public int Number { get; set; } = default!;
    public decimal Amount { get; set; } = default!;
    public string PaymentSchedule { get; set; } = default!;
    public bool IsOptional { get; set; }
    public bool IsLoan { get; set; }
    public bool IsActive { get; set; }
}