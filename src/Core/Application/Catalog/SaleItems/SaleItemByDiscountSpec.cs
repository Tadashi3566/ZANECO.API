namespace ZANECO.API.Application.Catalog.SaleItems;

public class SaleItemByDiscountSpec : Specification<SaleItem>, ISingleResultSpecification<SaleItem>
{
    public SaleItemByDiscountSpec(DefaultIdType id) =>
        Query.Where(b => b.DiscountId == id);
}