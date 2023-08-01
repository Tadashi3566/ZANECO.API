namespace ZANECO.API.Application.Catalog.Customers;

public class CustomerByCodeSpec : Specification<Customer, CustomerDto>, ISingleResultSpecification<Customer>
{
    public CustomerByCodeSpec(string code) =>
        Query.Where(b => b.Code == code);
}