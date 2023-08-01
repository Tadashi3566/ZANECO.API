namespace ZANECO.API.Application.Catalog.Sales;

public class SaleByCustomerSpec : Specification<Sale>, ISingleResultSpecification<Sale>
{
    public SaleByCustomerSpec(DefaultIdType id) =>
        Query.Where(b => b.CustomerId == id);
}