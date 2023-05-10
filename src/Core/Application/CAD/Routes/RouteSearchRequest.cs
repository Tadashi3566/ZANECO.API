using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Routes;

public class RouteSearchRequest : PaginationFilter, IRequest<PaginationResponse<RouteDto>>
{
}

public class RoutesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Route, RouteDto>
{
    public RoutesBySearchRequestSpec(RouteSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class RouteSearchRequestHandler : IRequestHandler<RouteSearchRequest, PaginationResponse<RouteDto>>
{
    private readonly IReadRepository<Route> _repository;

    public RouteSearchRequestHandler(IReadRepository<Route> repository) => _repository = repository;

    public async Task<PaginationResponse<RouteDto>> Handle(RouteSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new RoutesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}