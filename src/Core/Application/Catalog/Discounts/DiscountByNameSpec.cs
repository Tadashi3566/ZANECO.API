namespace ZANECO.API.Application.Catalog.Discounts;

public class DiscountByNameSpec : Specification<Discount>, ISingleResultSpecification
{
    public DiscountByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}