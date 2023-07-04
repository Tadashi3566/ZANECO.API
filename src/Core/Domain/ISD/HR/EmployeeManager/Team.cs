namespace ZANECO.API.Domain.ISD.HR.EmployeeManager;

public class Team : AuditableEntity, IAggregateRoot
{
    public virtual Employee? Employee { get; private set; }
    public DefaultIdType ManagerId { get; private set; }
    public string ManagerName { get; private set; }
    public DefaultIdType MemberId { get; private set; }
    public string MemberName { get; private set; }

    public Team(DefaultIdType managerId, string managerName, DefaultIdType memberId, string memberName, string? description, string? notes)
    {
        ManagerId = managerId;
        ManagerName = managerName;
        MemberId = memberId;
        MemberName = memberName;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();
    }

    public Team Update(string managerName, string memberName, string? description, string? notes)
    {
        if (!ManagerName.Equals(managerName)) ManagerName = managerName;
        if (!MemberName.Equals(memberName)) MemberName = memberName;

        if (description is not null && (Description is null || !Description!.Equals(description))) Description = description.Trim();
        if (notes is not null && (Notes is null || !Notes!.Equals(notes))) Notes = notes.Trim();

        return this;
    }
}