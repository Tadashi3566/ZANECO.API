namespace ZANECO.API.Domain.Surveys;

public class Rate : AuditableEntity, IAggregateRoot
{
    public int Number { get; private set; }

    public Rate(int number, string name, string? description = "", string? notes = "")
    {
        Number = number;
        Name = name.Trim().ToUpper();
        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
    }

    public Rate Update(int number, string name, string? description = "", string? notes = "")
    {
        if (number is not 0 && !Number.Equals(number)) Number = number;
        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();
        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
        return this;
    }
}