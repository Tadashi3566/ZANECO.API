namespace ZANECO.API.Application.CAD.PowerConsumptions;

public class PowerConsumptionDto : DtoExtension, IDto
{
    public DefaultIdType GroupId { get; set; } = default!;
    public string GroupCode { get; set; } = default!;
    public string GroupName { get; set; } = default!;
    public string BillMonth { get; set; } = default!;
    public decimal KWHPurchased { get; set; } = default!;


}