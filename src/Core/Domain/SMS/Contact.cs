namespace ZANECO.API.Domain.SMS;

public class Contact : AuditableEntity, IAggregateRoot
{
    public string ContactType { get; private set; } = default!;
    public string Reference { get; private set; } = default!;
    public string PhoneNumber { get; private set; } = default!;

    public string Address { get; private set; } = default!;

    public Contact(string contactType, string reference, string phoneNumber, string name, string address, string? description = null, string? notes = null, string? imagePath = null)
    {
        ContactType = contactType.ToUpper();
        Reference = reference.Trim().ToUpper();
        PhoneNumber = phoneNumber.Trim().ToUpper();
        Name = name.Trim().ToUpper();
        Address = address.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public Contact Update(string contactType, string reference, string phoneNumber, string name, string address, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (contactType is not null && !ContactType.Equals(contactType)) ContactType = contactType.Trim().ToUpper();
        if (reference is not null && !Reference.Equals(reference)) Reference = reference.Trim().ToUpper();
        if (phoneNumber is not null && !PhoneNumber.Equals(phoneNumber)) PhoneNumber = phoneNumber.Trim();
        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();
        if (address is not null && !Address.Equals(address)) Address = address.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public Contact ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}