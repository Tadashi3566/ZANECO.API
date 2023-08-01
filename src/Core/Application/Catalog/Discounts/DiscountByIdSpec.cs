namespace ZANECO.API.Application.Catalog.Discounts;

public class DiscountByIdSpec : Specification<Discount>, ISingleResultSpecification<Discount>
{
    public DiscountByIdSpec(DefaultIdType id) =>
        Query.Where(b => b.Id == id);
}