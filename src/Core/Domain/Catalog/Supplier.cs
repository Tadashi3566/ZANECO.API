namespace ZANECO.API.Domain.Catalog;

public class Supplier : AuditableEntity, IAggregateRoot
{
    public Supplier()
    {
    }

    public string Address { get; private set; } = default!;
    public string Tin { get; private set; } = default!;
    public string Agent { get; private set; } = default!;
    public string PhoneNumber { get; private set; } = default!;

    public Supplier(string name, string address, string tin, string agent, string phoneNumber, string? description = null, string? notes = null, string? imagePath = null)
    {
        Name = name.Trim().ToUpper();
        Address = address;
        Tin = tin;
        Agent = agent;
        PhoneNumber = phoneNumber;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public Supplier Update(string name, string address, string tin, string agent, string phoneNumber, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();
        if (address is not null && !Address.Equals(address)) Address = address;
        if (tin is not null && !Tin.Equals(Tin)) Tin = tin;
        if (agent is not null && !Agent.Equals(agent)) Agent = agent;
        if (phoneNumber is not null && !PhoneNumber.Equals(phoneNumber)) PhoneNumber = phoneNumber;

        if (description is not null && Description?.Equals(description) is false) Description = description.Trim();
        if (notes is not null && Notes?.Equals(notes) is false) Notes = notes.Trim();

        if (!string.IsNullOrEmpty(imagePath) && ImagePath?.Equals(imagePath) is false) if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public Supplier ClearImagePath()
    {
        ImagePath = null;

        return this;
    }
}