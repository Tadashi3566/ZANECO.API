using ZANECO.API.Application.Catalog.Brands;

namespace ZANECO.API.Application.Catalog.Products;

public class ProductDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public string? ImagePath { get; set; }
    public BrandDto Brand { get; set; } = default!;
}