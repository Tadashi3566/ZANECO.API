using Mapster;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public class TeamSearchViaDapperRequest : PaginationFilter, IRequest<PaginationResponse<TeamDetailDto>>
{
    public DefaultIdType? LeaderId { get; set; }
}

public class TeamSearchViaDapperRequestHandler : IRequestHandler<TeamSearchViaDapperRequest, PaginationResponse<TeamDetailDto>?>
{
    private readonly IDapperRepository _repository;

    public TeamSearchViaDapperRequestHandler(IDapperRepository repository) => _repository = repository;

    public async Task<PaginationResponse<TeamDetailDto>?> Handle(TeamSearchViaDapperRequest request, CancellationToken cancellationToken)
    {
        string sqlQuery = @"SELECT t.*, e.*
                            FROM datazaneco.Teams AS t
                            INNER JOIN datazaneco.Employees AS e
                            ON t.EmployeeId = e.Id
                            WHERE t.TenantId = '@tenant' AND t.LeaderId = @LeaderId";

        var parameters = new { LeaderId = request.LeaderId };

        var teams = await _repository.QueryListAsync<Team>(sqlQuery, parameters, cancellationToken: cancellationToken);

        var result = teams.Adapt<PaginationResponse<TeamDetailDto>>();

        return result;
    }
}