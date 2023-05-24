namespace ZANECO.API.Application.Catalog.SaleItems;

public class SaleItemDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType SaleId { get; private set; }
    public DefaultIdType ProductId { get; private set; }
    public DefaultIdType BarcodeId { get; private set; }
    public DefaultIdType DiscountId { get; private set; }
    public DateTime Date { get; set; } = default!;
    public string Transaction { get; set; } = default!;
    public int Items { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal Gross { get; set; } = default!;
    public decimal Vat { get; set; } = default!;
    public decimal DiscountAmount { get; set; } = default!;
    public decimal Net { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
    public string? ImagePath { get; set; }
}