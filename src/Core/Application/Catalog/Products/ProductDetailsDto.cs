using ZANECO.API.Application.Catalog.Brands;

namespace ZANECO.API.Application.Catalog.Products;

public class ProductDetailsDto : BaseDto, IDto
{
    public BrandDto Brand { get; set; } = default!;
    public decimal Rate { get; set; }
}