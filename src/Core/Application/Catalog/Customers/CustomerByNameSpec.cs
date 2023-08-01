namespace ZANECO.API.Application.Catalog.Customers;

public class CustomerByNameSpec : Specification<Customer, CustomerDto>, ISingleResultSpecification<Customer>
{
    public CustomerByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}