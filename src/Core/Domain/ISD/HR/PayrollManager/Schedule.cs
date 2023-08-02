namespace ZANECO.API.Domain.ISD.HR.PayrollManager;

public class Schedule : AuditableEntity, IAggregateRoot
{
    public Schedule(string name, string? description = null, string? notes = null)
    {
        Name = name.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
    }

    public Schedule Update(string name, string? description = null, string? notes = null)
    {
        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        return this;
    }
}