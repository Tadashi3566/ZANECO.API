using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Barangays;

public class BarangaySearchRequest : PaginationFilter, IRequest<PaginationResponse<BarangayDto>>
{
}

public class BarangaysBySearchRequestSpec : EntitiesByPaginationFilterSpec<Barangay, BarangayDto>
{
    public BarangaysBySearchRequestSpec(BarangaySearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class BarangaySearchRequestHandler : IRequestHandler<BarangaySearchRequest, PaginationResponse<BarangayDto>>
{
    private readonly IReadRepository<Barangay> _repository;

    public BarangaySearchRequestHandler(IReadRepository<Barangay> repository) => _repository = repository;

    public async Task<PaginationResponse<BarangayDto>> Handle(BarangaySearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new BarangaysBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}