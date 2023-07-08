using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public class TeamUpdateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType LeaderId { get; set; } = default!;
    public DefaultIdType EmployeeId { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class TeamUpdateRequestValidator : CustomValidator<TeamUpdateRequest>
{
    public TeamUpdateRequestValidator()
    {
        RuleFor(p => p.LeaderId)
            .NotEmpty();

        RuleFor(p => p.EmployeeId)
            .NotEmpty();
    }
}

public class TeamUpdateRequestHandler : IRequestHandler<TeamUpdateRequest, DefaultIdType>
{
    private readonly IReadRepository<Employee> _repoEmployee;
    private readonly IRepositoryWithEvents<Team> _repoTeam;

    public TeamUpdateRequestHandler(IReadRepository<Employee> repoEmployee, IRepositoryWithEvents<Team> repoTeam) =>
        (_repoEmployee, _repoTeam) = (repoEmployee, repoTeam);

    public async Task<DefaultIdType> Handle(TeamUpdateRequest request, CancellationToken cancellationToken)
    {
        var team = await _repoTeam.GetByIdAsync(request.Id, cancellationToken);
        _ = team ?? throw new NotFoundException("Team not found.");

        // Get Manager Information
        var manager = await _repoEmployee.GetByIdAsync(request.LeaderId, cancellationToken);
        _ = manager ?? throw new NotFoundException("Employee not found.");
        if (!manager.IsActive) throw new ArgumentException("Employee is no longer Active");

        // Get Member Information
        var member = await _repoEmployee.GetByIdAsync(request.EmployeeId, cancellationToken);
        _ = member ?? throw new NotFoundException("Employee not found.");
        if (!member.IsActive) throw new ArgumentException("Employee is no longer Active");

        var updatedTeam = team.Update(manager.FullInitialName(), member.FullInitialName(), member.Department, member.Position, request.Description, request.Notes);

        await _repoTeam.UpdateAsync(updatedTeam, cancellationToken);

        return request.Id;
    }
}