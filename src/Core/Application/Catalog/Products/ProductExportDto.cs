namespace ZANECO.API.Application.Catalog.Products;

public class ProductExportDto : IDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Rate { get; set; } = default!;
    public string BrandName { get; set; } = default!;
}