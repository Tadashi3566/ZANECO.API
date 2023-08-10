namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public class TeamDto : BaseDto, IDto
{
    public DefaultIdType LeaderId { get; set; } = default!;
    public string LeaderName { get; set; } = default!;

    public DefaultIdType EmployeeId { get; set; } = default!;
    public string EmployeeName { get; set; } = default!;
}