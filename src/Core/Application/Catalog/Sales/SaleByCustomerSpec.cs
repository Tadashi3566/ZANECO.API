namespace ZANECO.API.Application.Catalog.Sales;

public class SaleByCustomerSpec : Specification<Sale>, ISingleResultSpecification
{
    public SaleByCustomerSpec(DefaultIdType id) =>
        Query.Where(b => b.CustomerId == id);
}