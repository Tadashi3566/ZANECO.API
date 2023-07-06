using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public class TeamDeleteRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }

    public TeamDeleteRequest(DefaultIdType id) => Id = id;
}

public class TeamDeleteRequestHandler : IRequestHandler<TeamDeleteRequest, DefaultIdType>
{
    private readonly IRepositoryWithEvents<Team> _repository;
    private readonly IStringLocalizer<TeamDeleteRequestHandler> _localizer;

    public TeamDeleteRequestHandler(IRepositoryWithEvents<Team> repository, IStringLocalizer<TeamDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<DefaultIdType> Handle(TeamDeleteRequest request, CancellationToken cancellationToken)
    {
        var team = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = team ?? throw new NotFoundException(_localizer["Team not found."]);

        await _repository.DeleteAsync(team, cancellationToken);

        return request.Id;
    }
}