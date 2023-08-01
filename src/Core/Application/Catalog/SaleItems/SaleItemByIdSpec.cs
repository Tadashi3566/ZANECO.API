namespace ZANECO.API.Application.Catalog.SaleItems;

public class SaleItemByIdSpec : Specification<SaleItem, SaleItem>, ISingleResultSpecification<SaleItem>
{
    public SaleItemByIdSpec(DefaultIdType id) =>
        Query.Where(b => b.Id == id);
}