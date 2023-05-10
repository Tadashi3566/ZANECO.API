namespace ZANECO.API.Application.Catalog.SaleItems;

public class SaleItemBySaleSpec : Specification<SaleItem>, ISingleResultSpecification
{
    public SaleItemBySaleSpec(DefaultIdType id) =>
        Query.Where(b => b.SaleId == id);
}