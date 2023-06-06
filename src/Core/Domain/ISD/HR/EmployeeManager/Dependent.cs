namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Dependent : AuditableEntity, IAggregateRoot
{
    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType EmployeeId { get; private set; }
    public string EmployeeName { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Gender { get; private set; } = default!;
    public DateTime? BirthDate { get; private set; }
    public string Relation { get; private set; } = default!;
    public string? ImagePath { get; private set; }

    public Dependent(DefaultIdType employeeId, string employeeName, string name, string gender, DateTime? birthDate, string relation, string? description, string? notes, string? imagePath)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;

        Name = name.Trim().ToUpper();
        Gender = gender;
        BirthDate = birthDate;
        Relation = relation;
        if (description is not null) Description = description.Trim();
        if (notes is not null) Notes = notes.Trim();
        ImagePath = imagePath;
    }

    public Dependent Update(DefaultIdType? employeeId, string employeeName, string name, string gender, DateTime? birthDate, string relation, string? description, string? notes, string? imagePath)
    {
        if (employeeId.HasValue && employeeId.Value != DefaultIdType.Empty && !EmployeeId.Equals(employeeId.Value)) EmployeeId = employeeId.Value;
        if (employeeName is not null && !EmployeeName.Equals(employeeName)) EmployeeName = employeeName.Trim().ToUpper();

        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();
        if (gender is not null && !Gender.Equals(gender)) Gender = gender;

        if (birthDate.HasValue && birthDate.Value != default && !BirthDate.Equals(birthDate.Value)) BirthDate = birthDate.Value;

        if (relation is not null && !Relation.Equals(relation)) Relation = relation;
        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        if (!string.IsNullOrEmpty(imagePath) && !ImagePath!.Equals(imagePath)) ImagePath = imagePath;
        return this;
    }

    public Dependent ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}