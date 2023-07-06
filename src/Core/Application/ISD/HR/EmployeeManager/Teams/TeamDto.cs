namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public class TeamDto : IDto
{
    public DefaultIdType Id { get; set; }

    public DefaultIdType LeaderId { get; set; } = default!;
    public string LeaderName { get; set; } = default!;

    public DefaultIdType EmployeeId { get; set; } = default!;
    public string EmployeeName { get; set; } = default!;

    public string? Description { get; set; }
    public string? Notes { get; set; }
}