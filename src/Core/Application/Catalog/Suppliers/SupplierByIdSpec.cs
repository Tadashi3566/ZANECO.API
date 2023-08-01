namespace ZANECO.API.Application.Catalog.Suppliers;

public class SupplierByIdSpec : Specification<Supplier, SupplierDto>, ISingleResultSpecification<Supplier>
{
    public SupplierByIdSpec(DefaultIdType id) =>
        Query.Where(b => b.Id == id);
}