using ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public class TeamDetailDto : IDto
{
    public DefaultIdType Id { get; set; }

    public EmployeeDto Employee { get; set; } = default!;
    public DefaultIdType LeaderId { get; set; } = default!;
    public string LeaderName { get; set; } = default!;
    //public string LeaderDepartment { get; set; } = default!;
    //public string LeaderDesignation { get; set; } = default!;

    public DefaultIdType EmployeeId { get; set; } = default!;
    public string EmployeeName { get; set; } = default!;
    //public string EmployeeDepartment { get; set; } = default!;
    //public string EmployeeDesignation { get; set; } = default!;
    //public string Department { get; set; } = default!;
    //public string Position { get; set; } = default!;

    public string? Description { get; set; }
    public string? Notes { get; set; }
}