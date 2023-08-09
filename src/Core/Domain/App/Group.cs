using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Domain.App;

public class Group : AuditableEntity, IAggregateRoot
{
    public string Application { get; private set; }
    public string Parent { get; private set; }
    public string Tag { get; private set; }
    public int Number { get; private set; }
    public string Code { get; private set; }

    public decimal Amount { get; private set; }
    public virtual Employee? Employee { get; set; }
    public Guid? EmployeeId { get; private set; }
    public string? EmployeeName { get; private set; }

    public Group(string application, string parent, string tag, int number, string code, string name, decimal amount, Guid? employeeId, string? employeeName, string? description = null, string? notes = null, string? imagePath = null)
    {
        Application = application.ToUpper();
        Parent = parent.Trim().ToUpper();
        Tag = tag.Trim().ToUpper();

        Number = number;
        Code = code.Trim().ToUpper();
        Name = name.Trim().ToUpper();
        Amount = amount;

        if (employeeId is not null && (EmployeeId is null || !EmployeeId!.Equals(employeeId))) EmployeeId = employeeId;
        if (employeeName is not null && (EmployeeName is null || !EmployeeName!.Equals(employeeName))) EmployeeName = employeeName;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
    }

    public Group Update(string application, string parent, string tag, int number, string code, string name, decimal amount, Guid? employeeId, string? employeeName, string? description = null, string? notes = null, string? imagePath = null)
    {
        if (!Application.Equals(application)) Application = application.ToUpper();
        if (!Parent.Equals(parent)) Parent = parent.Trim().ToUpper();
        if (!Tag.Equals(tag)) Tag = tag.Trim().ToUpper();

        if (!Number.Equals(number)) Number = number;
        if (!Code.Equals(code)) Code = code.Trim().ToUpper();
        if (!Name.Equals(name)) Name = name.Trim().ToUpper();
        if (!Amount.Equals(amount)) Amount = amount;

        if (!EmployeeId!.Equals(employeeId)) EmployeeId = employeeId;
        if (!EmployeeName!.Equals(employeeName)) EmployeeName = employeeName;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
        if (imagePath is not null && (ImagePath?.Equals(imagePath) != true)) ImagePath = imagePath;
        return this;
    }

    public Group ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}