using ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public class TeamDetailDto : BaseDto, IDto
{
    public EmployeeDto Employee { get; set; } = default!;
    public DefaultIdType LeaderId { get; set; } = default!;
    public string LeaderName { get; set; } = default!;

    public DefaultIdType EmployeeId { get; set; } = default!;
    public string EmployeeName { get; set; } = default!;

    public string? Department { get; set; }
    public string? Position { get; set; }
}