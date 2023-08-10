namespace ZANECO.API.Application.Catalog.SaleItems;

public class SaleItemDto : DtoExtension, IDto
{
    public DefaultIdType SaleId { get; private set; }
    public DefaultIdType ProductId { get; private set; }
    public DefaultIdType BarcodeId { get; private set; }
    public DefaultIdType DiscountId { get; private set; }
    public DateTime Date { get; set; } = default!;
    public string Transaction { get; set; } = default!;
    public int Items { get; set; } = default!;
    public decimal Gross { get; set; } = default!;
    public decimal Vat { get; set; } = default!;
    public decimal DiscountAmount { get; set; } = default!;
    public decimal Net { get; set; } = default!;


}