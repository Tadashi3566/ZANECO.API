using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public class TeamGetRequest : IRequest<TeamDetailDto>
{
    public DefaultIdType Id { get; set; }

    public TeamGetRequest(DefaultIdType id) => Id = id;
}

public class TeamGetRequestHandler : IRequestHandler<TeamGetRequest, TeamDetailDto>
{
    private readonly IRepository<Team> _repoTeam;
    private readonly IRepository<Employee> _repoEmployee;

    public TeamGetRequestHandler(IRepository<Team> repoTeam, IRepository<Employee> repoEmployee) =>
        (_repoTeam, _repoEmployee) = (repoTeam, repoEmployee);

    public async Task<TeamDetailDto> Handle(TeamGetRequest request, CancellationToken cancellationToken)
    {
        var team = await _repoTeam.GetByIdAsync(request.Id, cancellationToken)
                   ?? throw new NotFoundException($"Team Id {request.Id} not found.");

        var leader = await _repoEmployee.GetByIdAsync(team.LeaderId, cancellationToken)
                   ?? throw new NotFoundException($"Leader Id {team.LeaderId} not found.");

        var employee = await _repoEmployee.GetByIdAsync(team.EmployeeId, cancellationToken)
                     ?? throw new NotFoundException($"Member Id {team.EmployeeId} not found.");

        return new TeamDetailDto()
        {
            Id = team.Id,
            LeaderId = team.LeaderId,
            LeaderName = team.LeaderName,
            LeaderDepartment = leader.Department!,
            LeaderDesignation = leader.Position!,
            EmployeeId = team.EmployeeId,
            EmployeeName = team.EmployeeName,
            EmployeeDepartment = employee.Department!,
            EmployeeDesignation = employee.Position!,
            Description = team.Description,
            Notes = team.Notes,
        };
    }
}