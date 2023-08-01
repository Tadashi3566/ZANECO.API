namespace ZANECO.API.Application.Catalog.Barcodes;

public class BarcodeBySpecificationSpec : Specification<Barcode>, ISingleResultSpecification<Barcode>
{
    public BarcodeBySpecificationSpec(string specification) =>
        Query.Where(b => b.Specification == specification);
}