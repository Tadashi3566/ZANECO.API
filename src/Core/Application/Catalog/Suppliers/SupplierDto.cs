namespace ZANECO.API.Application.Catalog.Suppliers;

public class SupplierDto : DtoExtension, IDto
{
    public string Address { get; set; } = default!;
    public string Tin { get; set; } = default!;
    public string Agent { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}