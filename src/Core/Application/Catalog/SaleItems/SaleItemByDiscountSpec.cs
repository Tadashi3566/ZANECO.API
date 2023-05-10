namespace ZANECO.API.Application.Catalog.SaleItems;

public class SaleItemByDiscountSpec : Specification<SaleItem>, ISingleResultSpecification
{
    public SaleItemByDiscountSpec(DefaultIdType id) =>
        Query.Where(b => b.DiscountId == id);
}