using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public class TeamCreateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType LeaderId { get; set; } = default!;
    public DefaultIdType EmployeeId { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class CreateTeamRequestValidator : CustomValidator<TeamCreateRequest>
{
    public CreateTeamRequestValidator()
    {
        RuleFor(r => r.LeaderId)
            .NotEmpty();

        RuleFor(r => r.EmployeeId)
            .NotEmpty();
    }
}

public class TeamCreateRequestHandler : IRequestHandler<TeamCreateRequest, DefaultIdType>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IRepositoryWithEvents<Team> _repoTeam;

    public TeamCreateRequestHandler(IReadRepository<Employee> repoEmployee, IRepositoryWithEvents<Team> repoTeam) =>
        (_repoEmployee, _repoTeam) = (repoEmployee, repoTeam);

    public async Task<DefaultIdType> Handle(TeamCreateRequest request, CancellationToken cancellationToken)
    {
        var existingMember =
            await _repoTeam.FirstOrDefaultAsync(
                new TeamByMemberSpec(request.LeaderId, request.EmployeeId),
                cancellationToken);

        if (existingMember is not null)
            throw new ArgumentException("Team Member already existing.");

        // Get Manager Information
        var manager = await _repoEmployee.GetByIdAsync(request.LeaderId, cancellationToken);
        _ = manager ?? throw new NotFoundException("Employee not found.");
        if (!manager.IsActive) throw new ArgumentException("Employee is no longer Active");

        // Get Member Information
        var member = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = member ?? throw new NotFoundException("Employee not found.");
        if (!member.IsActive) throw new ArgumentException("Employee is no longer Active");

        var team = new Team(request.LeaderId, manager.FullInitialName(), request.EmployeeId, member.FullInitialName(), request.Description, request.Notes);

        await _repoTeam.AddAsync(team, cancellationToken);

        return team.Id;
    }
}