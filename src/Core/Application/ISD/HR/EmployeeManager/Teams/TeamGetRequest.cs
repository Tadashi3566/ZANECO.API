using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public class TeamGetRequest : IRequest<TeamDto>
{
    public DefaultIdType Id { get; set; }

    public TeamGetRequest(DefaultIdType id) => Id = id;
}

public class TeamGetRequestHandler : IRequestHandler<TeamGetRequest, TeamDto>
{
    private readonly IRepository<Team> _repository;
    private readonly IStringLocalizer<TeamGetRequestHandler> _localizer;

    public TeamGetRequestHandler(IRepository<Team> repository, IStringLocalizer<TeamGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<TeamDto> Handle(TeamGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(new TeamByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Team not found."], request.Id));
}