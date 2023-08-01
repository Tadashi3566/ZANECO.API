namespace ZANECO.API.Application.Catalog.Brands;

public class BrandByNameSpec : Specification<Brand, BrandDto>, ISingleResultSpecification<Brand>
{
    public BrandByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}