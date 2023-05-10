namespace ZANECO.API.Application.Catalog.Barcodes;

public class BarcodeByIdSpec : Specification<Barcode>, ISingleResultSpecification
{
    public BarcodeByIdSpec(DefaultIdType id) =>
        Query.Where(b => b.Id == id);
}