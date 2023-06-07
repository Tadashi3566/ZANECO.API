namespace ZANECO.API.Domain.Catalog;

public class Supplier : AuditableEntity, IAggregateRoot
{
    public Supplier()
    {
    }

    public string Name { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string Tin { get; private set; } = default!;
    public string Agent { get; private set; } = default!;
    public string PhoneNumber { get; private set; } = default!;
    public string? ImagePath { get; private set; }

    public Supplier(string name, string address, string tin, string agent, string phoneNumber, string? description, string? notes, string? imagePath)
    {
        Name = name.Trim().ToUpper();
        Address = address;
        Tin = tin;
        Agent = agent;
        PhoneNumber = phoneNumber;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        ImagePath = imagePath;
    }

    public Supplier Update(string name, string address, string tin, string agent, string phoneNumber, string? description, string? notes, string? imagePath)
    {
        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();
        if (address is not null && !Address.Equals(address)) Address = address;
        if (tin is not null && !Tin.Equals(Tin)) Tin = tin;
        if (agent is not null && !Agent.Equals(agent)) Agent = agent;
        if (phoneNumber is not null && !PhoneNumber.Equals(phoneNumber)) PhoneNumber = phoneNumber;

        if (description is not null && Description?.Equals(description) is false) Description = description.Trim();
        if (notes is not null && Notes?.Equals(notes) is false) Notes = notes.Trim();

        if (!string.IsNullOrEmpty(imagePath) && ImagePath?.Equals(imagePath) is false) ImagePath = imagePath;

        return this;
    }

    public Supplier ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}