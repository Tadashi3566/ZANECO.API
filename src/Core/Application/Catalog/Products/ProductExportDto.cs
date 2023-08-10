namespace ZANECO.API.Application.Catalog.Products;

public class ProductExportDto : DtoExtension, IDto
{
    public decimal Rate { get; set; } = default!;
    public string BrandName { get; set; } = default!;
}