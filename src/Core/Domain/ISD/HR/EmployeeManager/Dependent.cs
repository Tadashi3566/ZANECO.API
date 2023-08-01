namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Dependent : AuditableEntity, IAggregateRoot
{
    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType EmployeeId { get; private set; }
    public string EmployeeName { get; private set; } = default!;

    public string Gender { get; private set; } = default!;
    public DateTime? BirthDate { get; private set; }
    public string Relation { get; private set; } = default!;
    public string? ImagePath { get; private set; }

    public Dependent(DefaultIdType employeeId, string employeeName, string name, string gender, DateTime? birthDate, string relation, string? description = null, string? notes = null, string? imagePath = null)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;

        Name = name.Trim().ToUpper();
        Gender = gender;
        BirthDate = birthDate;
        Relation = relation;
        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public Dependent Update(DefaultIdType? employeeId, string employeeName, string name, string gender, DateTime? birthDate, string relation, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (employeeId.HasValue && employeeId.Value != DefaultIdType.Empty && !EmployeeId.Equals(employeeId.Value)) EmployeeId = employeeId.Value;
        if (employeeName is not null && !EmployeeName.Equals(employeeName)) EmployeeName = employeeName.Trim().ToUpper();

        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();
        if (gender is not null && !Gender.Equals(gender)) Gender = gender;

        if (birthDate.HasValue && birthDate.Value != default && !BirthDate.Equals(birthDate.Value)) BirthDate = birthDate.Value;

        if (relation is not null && !Relation.Equals(relation)) Relation = relation;
        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
        return this;
    }

    public Dependent ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}