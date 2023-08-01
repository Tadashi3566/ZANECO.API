namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Employer : AuditableEntity, IAggregateRoot
{
    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType EmployeeId { get; private set; }
    public string EmployeeName { get; private set; } = default!;

    public string Address { get; private set; } = default!;
    public string Designation { get; private set; } = default!;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    public string? ImagePath { get; private set; }

    public Employer(DefaultIdType employeeId, string employeeName, string name, string address, string designation, DateTime startDate, DateTime endDate, string? description, string? notes, string? imagePath)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;

        Name = name.Trim().ToUpper();
        Address = address;
        Designation = designation;

        StartDate = startDate;
        EndDate = endDate;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;
    }

    public Employer Update(DefaultIdType? employeeId, string employeeName, string name, string address, string designation, DateTime startDate, DateTime endDate, string? description, string? notes, string? imagePath)
    {
        if (employeeId.HasValue && employeeId.Value != DefaultIdType.Empty && !EmployeeId.Equals(employeeId.Value)) EmployeeId = employeeId.Value;
        if (!EmployeeName.Equals(employeeName)) EmployeeName = employeeName;

        if (!Name.Equals(name)) Name = name.Trim().ToUpper();
        if (!Address.Equals(address)) Address = address;
        if (!Designation.Equals(designation)) Designation = designation;

        if (!StartDate.Equals(startDate)) StartDate = startDate;
        if (!EndDate.Equals(endDate)) EndDate = endDate;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;
        return this;
    }

    public Employer ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}