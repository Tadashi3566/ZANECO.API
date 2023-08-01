namespace ZANECO.API.Application.Catalog.SaleItems;

public class SaleItemByProductSpec : Specification<SaleItem>, ISingleResultSpecification<SaleItem>
{
    public SaleItemByProductSpec(DefaultIdType id) =>
        Query.Where(b => b.ProductId == id);
}