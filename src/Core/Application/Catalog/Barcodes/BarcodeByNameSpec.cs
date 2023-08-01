namespace ZANECO.API.Application.Catalog.Barcodes;

public class BarcodeByNameSpec : Specification<Barcode>, ISingleResultSpecification<Barcode>
{
    public BarcodeByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}