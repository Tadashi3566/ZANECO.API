namespace ZANECO.API.Application.SMS.Contacts;

public class ContactDto : DtoExtension, IDto
{
    public string ContactType { get; set; } = default!;
    public string Reference { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
}