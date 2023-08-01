namespace ZANECO.API.Application.Catalog.Barcodes;

public class BarcodeByCodeSpec : Specification<Barcode>, ISingleResultSpecification<Barcode>
{
    public BarcodeByCodeSpec(string code) =>
        Query.Where(b => b.Code == code);
}