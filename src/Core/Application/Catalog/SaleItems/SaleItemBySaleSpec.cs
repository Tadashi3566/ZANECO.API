namespace ZANECO.API.Application.Catalog.SaleItems;

public class SaleItemBySaleSpec : Specification<SaleItem, SaleItemDto>, ISingleResultSpecification<SaleItem>
{
    public SaleItemBySaleSpec(DefaultIdType id) =>
        Query.Where(b => b.SaleId == id);
}