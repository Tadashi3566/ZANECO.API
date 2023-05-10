namespace ZANECO.API.Application.Catalog.Barcodes;

public class BarcodeByNameSpec : Specification<Barcode>, ISingleResultSpecification
{
    public BarcodeByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}