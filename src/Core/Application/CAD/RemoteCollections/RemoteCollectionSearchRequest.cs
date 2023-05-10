using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.RemoteCollections;

public class RemoteCollectionSearchRequest : PaginationFilter, IRequest<PaginationResponse<RemoteCollectionDto>>
{
}

public class RemoteCollectionsBySearchRequestSpec : EntitiesByPaginationFilterSpec<RemoteCollection, RemoteCollectionDto>
{
    public RemoteCollectionsBySearchRequestSpec(RemoteCollectionSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class RemoteCollectionSearchRequestHandler : IRequestHandler<RemoteCollectionSearchRequest, PaginationResponse<RemoteCollectionDto>>
{
    private readonly IReadRepository<RemoteCollection> _repository;

    public RemoteCollectionSearchRequestHandler(IReadRepository<RemoteCollection> repository) => _repository = repository;

    public async Task<PaginationResponse<RemoteCollectionDto>> Handle(RemoteCollectionSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new RemoteCollectionsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}