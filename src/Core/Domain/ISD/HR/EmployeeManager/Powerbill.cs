namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Powerbill : AuditableEntity, IAggregateRoot
{
    public virtual Employee Employee { get; private set; } = default!;
    public DefaultIdType EmployeeId { get; private set; }
    public string EmployeeName { get; private set; } = default!;

    public string Account { get; private set; } = default!;
    public string Meter { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string? ImagePath { get; private set; }

    public Powerbill(DefaultIdType employeeId, string employeeName, string account, string meter, string name, string address, string? description, string? notes, string? imagePath)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;

        Account = account;
        Meter = meter;
        Name = name.Trim().ToUpper();
        Address = address;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;
    }

    public Powerbill Update(DefaultIdType? employeeId, string employeeName, string account, string meter, string name, string address, string? description, string? notes, string? imagePath)
    {
        if (employeeId.HasValue && employeeId.Value != DefaultIdType.Empty && !EmployeeId.Equals(employeeId.Value)) EmployeeId = employeeId.Value;
        if (employeeName is not null && !EmployeeName.Equals(employeeName)) EmployeeName = employeeName;

        if (account is not null && !Account.Equals(account)) Account = account;
        if (meter is not null && !Meter.Equals(meter)) Meter = meter;
        if (name is not null && !Name.Equals(name)) Name = name.Trim().ToUpper();
        if (address is not null && !Address.Equals(address)) Address = address;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        if (imagePath is not null && (ImagePath is null || !ImagePath!.Equals(imagePath))) ImagePath = imagePath;
        return this;
    }

    public Powerbill ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}