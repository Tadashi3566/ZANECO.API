using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Winners;

public class WinnerSearchRequest : PaginationFilter, IRequest<PaginationResponse<WinnerDto>>
{
}

public class WinnerBySearchRequestSpec : EntitiesByPaginationFilterSpec<Winner, WinnerDto>
{
    public WinnerBySearchRequestSpec(WinnerSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Id, !request.HasOrderBy());
}

public class WinnerSearchRequestHandler : IRequestHandler<WinnerSearchRequest, PaginationResponse<WinnerDto>>
{
    private readonly IReadRepository<Winner> _repository;

    public WinnerSearchRequestHandler(IReadRepository<Winner> repository) => _repository = repository;

    public async Task<PaginationResponse<WinnerDto>> Handle(WinnerSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new WinnerBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}