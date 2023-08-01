namespace ZANECO.API.Application.Catalog.Products;

public class ProductByIdWithBrandSpec : Specification<Product, ProductDetailsDto>, ISingleResultSpecification<Product>
{
    public ProductByIdWithBrandSpec(DefaultIdType id) =>
        Query
            .Include(p => p.Brand)
            .Where(p => p.Id == id);
}