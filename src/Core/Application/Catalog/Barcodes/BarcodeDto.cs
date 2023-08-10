namespace ZANECO.API.Application.Catalog.Barcodes;

public class BarcodeDto : DtoExtension, IDto
{
    public DefaultIdType ProductId { get; set; }
    public string Code { get; set; } = default!;
    public string Specification { get; set; } = default!;
    public string UnitOfMeasurement { get; set; } = default!;


}