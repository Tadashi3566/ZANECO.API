namespace ZANECO.API.Application.Catalog.SaleItems;

public class SaleItemByarcodeSpec : Specification<SaleItem, SaleItemDto>, ISingleResultSpecification<SaleItem>
{
    public SaleItemByarcodeSpec(DefaultIdType id) =>
        Query.Where(b => b.BarcodeId == id);
}