namespace ZANECO.API.Application.Catalog.Barcodes;

public class BarcodeDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType ProductId { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Specification { get; set; } = default!;
    public string UnitOfMeasurement { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
    public string? ImagePath { get; set; }
}