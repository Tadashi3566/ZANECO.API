namespace ZANECO.API.Domain.CAD;

public class Area : AuditableEntity, IAggregateRoot
{
    public Area()
    {
    }

    public int Number { get; private set; }
    public string Code { get; private set; } = default!;

    public Area(int number, string code, string name, string? description = null, string? notes = null)
    {
        Number = number;
        Code = code;
        Name = name.Trim().ToUpper();
        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
    }

    public Area Update(int number, string code, string name, string? description = null, string? notes = null)
    {
        if (Number != number) Number = number;
        if (!Code.Equals(code)) Code = code;
        if (!Name.Equals(name)) Name = name.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        return this;
    }
}