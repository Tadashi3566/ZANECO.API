namespace ZANECO.API.Application.Catalog.Sales;

public class SaleByOrNumerSpec : Specification<Sale, SaleDto>, ISingleResultSpecification<Sale>
{
    public SaleByOrNumerSpec(double orNumer) =>
        Query.Where(b => b.OrNumber == orNumer);
}