namespace ZANECO.API.Application.Catalog.Discounts;

public class DiscountByIdSpec : Specification<Discount>, ISingleResultSpecification
{
    public DiscountByIdSpec(DefaultIdType id) =>
        Query.Where(b => b.Id == id);
}