﻿namespace ZANECO.API.Domain.ISD.HR.PayrollManager;

public class Schedule : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;

    public Schedule(string name, string? description = "", string? notes = "")
    {
        Name = name.Trim().ToUpper();

        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();
    }

    public Schedule Update(string name, string? description = "", string? notes = "")
    {
        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();

        if (description is not null && !Description!.Equals(description)) Description = description.Trim();
        if (notes is not null && !Notes!.Equals(notes)) Notes = notes.Trim();

        return this;
    }
}