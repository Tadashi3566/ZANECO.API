using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public class TeamSearchRequest : PaginationFilter, IRequest<PaginationResponse<TeamDetailDto>>
{
    public DefaultIdType? LeaderId { get; set; }
}

public sealed class TeamsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Team, TeamDetailDto>
{
    public TeamsBySearchRequestSpec(TeamSearchRequest request)
        : base(request)
    {
        Query
            .Include(x => x.Employee)
                //.ThenInclude(e => e!.Department)
            .OrderBy(x => x.EmployeeName)
            .Where(x => x.LeaderId.Equals(request.LeaderId!.Value), request.LeaderId.HasValue);
    }
}

public class TeamSearchRequestHandler : IRequestHandler<TeamSearchRequest, PaginationResponse<TeamDetailDto>>
{
    private readonly IReadRepository<Team> _repository;

    public TeamSearchRequestHandler(IReadRepository<Team> repository) => _repository = repository;

    public async Task<PaginationResponse<TeamDetailDto>> Handle(TeamSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new TeamsBySearchRequestSpec(request);
        var result = await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
        return result;
    }
}