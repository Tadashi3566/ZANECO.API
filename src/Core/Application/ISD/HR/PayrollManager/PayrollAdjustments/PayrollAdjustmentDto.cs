namespace ZANECO.API.Application.ISD.HR.PayrollManager.PayrollAdjustments;

public class PayrollAdjustmentDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType PayrollId { get; set; } = default!;
    public string PayrollName { get; set; } = default!;
    public DefaultIdType AdjustmentId { get; set; } = default!;
    public int AdjustmentNumber { get; set; } = default!;
    public string AdjustmentName { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
}