namespace ZANECO.API.Application.Catalog.Discounts;

public class DiscountByNameSpec : Specification<Discount, DiscountDto>, ISingleResultSpecification<Discount>
{
    public DiscountByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}