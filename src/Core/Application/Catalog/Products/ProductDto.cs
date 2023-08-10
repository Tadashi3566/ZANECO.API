namespace ZANECO.API.Application.Catalog.Products;

public class ProductDto : DtoExtension, IDto
{
    public DefaultIdType BrandId { get; set; }
    public string BrandName { get; set; } = default!;
    public decimal Rate { get; set; }
}