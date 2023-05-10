namespace ZANECO.API.Application.CAD.PowerConsumptions;

public class PowerConsumptionDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType GroupId { get; set; } = default!;
    public string GroupCode { get; set; } = default!;
    public string GroupName { get; set; } = default!;
    public string BillMonth { get; set; } = default!;
    public decimal KWHPurchased { get; set; } = default!;

    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
}