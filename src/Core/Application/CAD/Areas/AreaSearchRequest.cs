using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Areas;

public class AreaSearchRequest : PaginationFilter, IRequest<PaginationResponse<AreaDto>>
{
}

public class AreasBySearchRequestSpec : EntitiesByPaginationFilterSpec<Area, AreaDto>
{
    public AreasBySearchRequestSpec(AreaSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Code, !request.HasOrderBy());
}

public class AreaSearchRequestHandler : IRequestHandler<AreaSearchRequest, PaginationResponse<AreaDto>>
{
    private readonly IReadRepository<Area> _repository;

    public AreaSearchRequestHandler(IReadRepository<Area> repository) => _repository = repository;

    public async Task<PaginationResponse<AreaDto>> Handle(AreaSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new AreasBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}