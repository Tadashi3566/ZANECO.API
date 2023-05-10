namespace ZANECO.API.Application.Catalog.Sales;

public class SaleByOrNumerSpec : Specification<Sale>, ISingleResultSpecification
{
    public SaleByOrNumerSpec(double orNumer) =>
        Query.Where(b => b.OrNumber == orNumer);
}