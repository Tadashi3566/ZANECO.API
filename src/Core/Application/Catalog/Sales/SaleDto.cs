namespace ZANECO.API.Application.Catalog.Sales;

public class SaleDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType CustomerId { get; set; }
    public DateTime Date { get; set; } = default!;
    public string Transaction { get; set; } = default!;
    public double OrNumber { get; set; } = default!;
    public int Items { get; set; } = default!;
    public decimal GrossSales { get; set; } = default!;
    public decimal TotalVat { get; set; } = default!;
    public decimal TotalDiscount { get; set; } = default!;
    public decimal NetSales { get; set; } = default!;
    public decimal Received { get; set; } = default!;
    public decimal Change { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
    public string? ImagePath { get; set; }
}