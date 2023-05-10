namespace ZANECO.API.Application.SMS.Contacts;

public class ContactDto : IDto
{
    public DefaultIdType Id { get; set; }
    public string ContactType { get; set; } = default!;
    public string Reference { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? Status { get; set; }
    public string? ImagePath { get; set; }
}