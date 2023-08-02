﻿namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class JobDescription : AuditableEntity, IAggregateRoot
{
    public JobDescription(string name, string? description = null, string? notes = null)
    {
        Name = name.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
    }

    public JobDescription Update(string name, string? description = null, string? notes = null)
    {
        if (!Name.Equals(name)) Name = name.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        return this;
    }
}