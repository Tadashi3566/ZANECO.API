namespace ZANECO.API.Application.Catalog.Discounts;

public class DiscountDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Name { get; set; } = default!;
    public float Percentage { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
}