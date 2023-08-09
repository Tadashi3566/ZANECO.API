namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class JobDescription : AuditableEntity, IAggregateRoot
{
    public int Rank { get; private set; } = default!;
    public int Number { get; private set; } = default!;
    public string Department { get; private set; } = default!;
    public string ReportsTo { get; private set; } = default!;

    public JobDescription(int rank, int number, string department, string reportsTo, string name, string? description = null, string? notes = null, string? imagePath = null)
    {
        Rank = rank;
        Number = number;

        Department = department;
        ReportsTo = reportsTo;

        Name = name.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public JobDescription Update(int rank, int number, string department, string reportsTo, string name, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (!Rank.Equals(rank)) Rank = rank;
        if (!Number.Equals(number)) Number = number;

        if (!Department.Equals(department)) Department = department;
        if (!ReportsTo.Equals(reportsTo)) ReportsTo = reportsTo;

        if (!Name.Equals(name)) Name = name.Trim().ToUpper();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;

        return this;
    }

    public JobDescription ClearImagePath()
    {
        ImagePath = string.Empty;

        return this;
    }
}