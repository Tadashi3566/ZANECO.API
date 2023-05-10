namespace ZANECO.API.Application.Catalog.Customers;

public class CustomerByNameSpec : Specification<Customer>, ISingleResultSpecification
{
    public CustomerByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}