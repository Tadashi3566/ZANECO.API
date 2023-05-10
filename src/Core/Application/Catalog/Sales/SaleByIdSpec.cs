namespace ZANECO.API.Application.Catalog.Sales;

public class SaleByIdSpec : Specification<Sale>, ISingleResultSpecification
{
    public SaleByIdSpec(DefaultIdType id) =>
        Query.Where(b => b.Id == id);
}