namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Team : AuditableEntity, IAggregateRoot
{
    public Employee Employee { get; private set; } = default!;
    public DefaultIdType LeaderId { get; private set; }
    public string LeaderName { get; private set; }
    public DefaultIdType EmployeeId { get; private set; }
    public string EmployeeName { get; private set; }
    public string? Department { get; private set; }
    public string? Position { get; private set; }

    public Team(DefaultIdType leaderId, string leaderName, DefaultIdType employeeId, string employeeName, string? department, string? position, string? description, string? notes)
    {
        LeaderId = leaderId;
        LeaderName = leaderName;
        EmployeeId = employeeId;
        EmployeeName = employeeName;

        if (department is not null && (Department?.Equals(department) != true)) Department = department.Trim();
        if (position is not null && (Position?.Equals(position) != true)) Position = position.Trim();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
    }

    public Team Update(string leaderName, string employeeName, string? department, string? position, string? description, string? notes)
    {
        if (!LeaderName.Equals(leaderName)) LeaderName = leaderName;
        if (!EmployeeName.Equals(employeeName)) EmployeeName = employeeName;

        if (department is not null && (Department?.Equals(department) != true)) Department = department.Trim();
        if (position is not null && (Position?.Equals(position) != true)) Position = position.Trim();

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        return this;
    }
}