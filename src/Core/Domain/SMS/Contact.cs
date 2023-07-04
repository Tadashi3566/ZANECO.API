namespace ZANECO.API.Domain.SMS;

public class Contact : AuditableEntity, IAggregateRoot
{
    public string ContactType { get; private set; } = default!;
    public string Reference { get; private set; } = default!;
    public string PhoneNumber { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string? ImagePath { get; private set; }

    public Contact(string contactType, string reference, string phoneNumber, string name, string address, string? description, string? notes, string? imagePath)
    {
        ContactType = contactType.ToUpper();
        Reference = reference.Trim().ToUpper();
        PhoneNumber = phoneNumber.Trim().ToUpper();
        Name = name.Trim().ToUpper();
        Address = address.Trim().ToUpper();

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;
    }

    public Contact Update(string contactType, string reference, string phoneNumber, string name, string address, string? description, string? notes, string? imagePath)
    {
        if (contactType is not null && !ContactType.Equals(contactType)) ContactType = contactType.Trim().ToUpper();
        if (reference is not null && !Reference.Equals(reference)) Reference = reference.Trim().ToUpper();
        if (phoneNumber is not null && !PhoneNumber.Equals(phoneNumber)) PhoneNumber = phoneNumber.Trim();
        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();
        if (address is not null && !Address.Equals(address)) Address = address.Trim().ToUpper();

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;

        return this;
    }

    public Contact ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}