namespace ZANECO.API.Application.Catalog.Products;

public class ProductsByBrandSpec : Specification<Product>
{
    public ProductsByBrandSpec(DefaultIdType brandId) =>
        Query.Where(p => p.BrandId == brandId);
}