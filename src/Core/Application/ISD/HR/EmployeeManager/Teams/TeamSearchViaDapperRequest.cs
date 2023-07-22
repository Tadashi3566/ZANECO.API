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
        //const string sqlQuery = "SELECT team.LeaderId, team.EmployeeId, emp.LastName, emp.FirstName, emp.Department, emp.Position FROM datazaneco.Teams AS team INNER JOIN datazaneco.Employees AS emp ON team.LeaderId = emp.Id WHERE team.TenantId = '@tenant' AND team.LeaderId = @LeaderId";
        //const string sqlQuery = "SELECT LeaderId, EmployeeId, Department FROM datazaneco.Teams WHERE TenantId = '@tenant' AND LeaderId = @LeaderId";

        const string sqlQuery = @"
            SELECT 
                team.LeaderId, 
                team.EmployeeId, 
                emp.Department, 
                emp.Department, 
                emp.Position 
            FROM 
                datazaneco.Teams AS team 
            INNER JOIN 
                datazaneco.Employees AS emp ON team.LeaderId = emp.Id 
            WHERE 
                team.TenantId = '@tenant'
                AND team.LeaderId = @LeaderId";

        var parameters = new { LeaderId = request.LeaderId };

        var teams = await _repository.QueryListAsync<Team>(sqlQuery, parameters, cancellationToken: cancellationToken);

        var result = teams.Adapt<PaginationResponse<TeamDetailDto>>();

        return result;
    }
}