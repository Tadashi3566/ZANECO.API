using Mapster;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public class TeamSearchViaDapperRequest : PaginationFilter, IRequest<List<TeamDto>>
{
    public DefaultIdType? ManagerId { get; set; }
}

public class TeamSearchViaDapperRequestHandler : IRequestHandler<TeamSearchViaDapperRequest, List<TeamDto>?>
{
    private readonly IDapperRepository _repository;

    public TeamSearchViaDapperRequestHandler(IDapperRepository repository) => _repository = repository;

    public async Task<List<TeamDto>?> Handle(TeamSearchViaDapperRequest request, CancellationToken cancellationToken)
    {
        var teams = await _repository.QueryListAsync<Team>(
        $"SELECT * FROM datazaneco.Teams WHERE TenantId = '@tenant' AND ManagerId = '{request.ManagerId}'", cancellationToken: cancellationToken);
        //$"SELECT * FROM datazaneco.\"Teams\" WHERE \"TenantId\" = '@tenant' AND \"ManagerId\" = '{request.ManagerId}'", cancellationToken: cancellationToken);

        return teams?.Adapt<List<TeamDto>>();
    }
}