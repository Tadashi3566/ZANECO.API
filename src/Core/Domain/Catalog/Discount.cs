namespace ZANECO.API.Domain.Catalog;

public class Discount : AuditableEntity, IAggregateRoot
{
    public Discount()
    {
    }

    public string Name { get; private set; } = default!;
    public float Percentage { get; private set; } = default!;

    public Discount(string name, float percentage, string? description = "", string? notes = "")
    {
        Name = name.Trim().ToUpper();
        Percentage = percentage;

        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();
    }

    public Discount Update(string name, float percentage, string? description = "", string? notes = "")
    {
        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();
        if (!Percentage.Equals(percentage)) Percentage = percentage;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        return this;
    }
}