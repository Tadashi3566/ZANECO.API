namespace ZANECO.API.Application.Catalog.Suppliers;

public class SupplierDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Tin { get; set; } = default!;
    public string Agent { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
    public string? ImagePath { get; set; }
}