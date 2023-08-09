namespace ZANECO.API.Application.ISD.HR.PayrollManager.PayrollAdjustments;

public class PayrollAdjustmentDto : DtoExtension<PayrollAdjustmentDto>, IDto
{
    public DefaultIdType PayrollId { get; set; } = default!;
    public string PayrollName { get; set; } = default!;
    public DefaultIdType AdjustmentId { get; set; } = default!;
    public int AdjustmentNumber { get; set; } = default!;
    public string AdjustmentName { get; set; } = default!;
}