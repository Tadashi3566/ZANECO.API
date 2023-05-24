namespace ZANECO.API.Domain.Catalog;

public class Brand : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; }

    public Brand(string name, string? description, string? notes)
    {
        Name = name;
        Description = description;
        Notes = notes;
    }

    public Brand Update(string? name, string? description, string? notes)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (notes is not null && Notes?.Equals(notes) is not true) Notes = notes;
        return this;
    }
}