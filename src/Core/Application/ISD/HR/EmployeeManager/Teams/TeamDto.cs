namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public class TeamDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType ManagerId { get; set; } = default!;
    public string ManagerName { get; set; } = default!;
    public DefaultIdType MemberId { get; set; } = default!;
    public string MemberName { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}