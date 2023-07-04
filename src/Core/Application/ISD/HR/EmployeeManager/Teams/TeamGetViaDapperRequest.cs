using Mapster;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public class TeamGetViaDapperRequest : IRequest<TeamDto>
{
    public DefaultIdType Id { get; set; }

    public TeamGetViaDapperRequest(DefaultIdType id) => Id = id;
}

public class TeamGetViaDapperRequestHandler : IRequestHandler<TeamGetViaDapperRequest, TeamDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<TeamGetViaDapperRequestHandler> _localizer;

    public TeamGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<TeamGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<TeamDto> Handle(TeamGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var team = await _repository.QueryFirstOrDefaultAsync<Team>(
            $"SELECT * FROM datazaneco.\"Teams\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = team ?? throw new NotFoundException(string.Format(_localizer["Team not found."], request.Id));

        return team.Adapt<TeamDto>();
    }
}