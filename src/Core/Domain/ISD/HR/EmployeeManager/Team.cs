namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Team : AuditableEntity, IAggregateRoot
{
    public virtual Employee? Employee { get; private set; }
    public DefaultIdType LeaderId { get; private set; }
    public string LeaderName { get; private set; }
    public DefaultIdType EmployeeId { get; private set; }
    public string EmployeeName { get; private set; }

    public Team(DefaultIdType leaderId, string leaderName, DefaultIdType employeeId, string employeeName, string? description, string? notes)
    {
        LeaderId = leaderId;
        LeaderName = leaderName;
        EmployeeId = employeeId;
        EmployeeName = employeeName;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
    }

    public Team Update(string leaderName, string employeeName, string? description, string? notes)
    {
        if (!LeaderName.Equals(leaderName)) LeaderName = leaderName;
        if (!EmployeeName.Equals(employeeName)) EmployeeName = employeeName;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        return this;
    }
}