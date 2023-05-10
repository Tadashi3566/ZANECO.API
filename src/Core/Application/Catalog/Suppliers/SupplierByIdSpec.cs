namespace ZANECO.API.Application.Catalog.Suppliers;

public class SupplierByIdSpec : Specification<Supplier>, ISingleResultSpecification
{
    public SupplierByIdSpec(DefaultIdType id) =>
        Query.Where(b => b.Id == id);
}