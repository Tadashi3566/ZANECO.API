namespace ZANECO.API.Application.Catalog.Customers;

public class CustomerDto : DtoExtension<CustomerDto>, IDto
{
    public string Code { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public decimal Investment { get; set; } = default!;
    public decimal Sales { get; set; } = default!;
    public int Points { get; set; } = default!;


}