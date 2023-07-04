using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Teams;

public class TeamSearchRequest : PaginationFilter, IRequest<PaginationResponse<TeamDto>>
{
    public DefaultIdType? ManagerId { get; set; }
}

public sealed class TeamsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Team, TeamDto>
{
    public TeamsBySearchRequestSpec(TeamSearchRequest request)
        : base(request) =>
        Query
            .Include(x => x.Employee)
            .OrderBy(x => x.MemberName)
            .Where(x => x.ManagerId.Equals(request.ManagerId!.Value), request.ManagerId.HasValue);
}

public class TeamSearchRequestHandler : IRequestHandler<TeamSearchRequest, PaginationResponse<TeamDto>>
{
    private readonly IReadRepository<Team> _repository;

    public TeamSearchRequestHandler(IReadRepository<Team> repository) => _repository = repository;

    public async Task<PaginationResponse<TeamDto>> Handle(TeamSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new TeamsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}