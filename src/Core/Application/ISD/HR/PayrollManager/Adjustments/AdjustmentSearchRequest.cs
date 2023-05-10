using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;

public class AdjustmentSearchRequest : PaginationFilter, IRequest<PaginationResponse<AdjustmentDto>>
{
}

public class AdjustmentsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Adjustment, AdjustmentDto>
{
    public AdjustmentsBySearchRequestSpec(AdjustmentSearchRequest request)
        : base(request) =>
        Query
            .OrderBy(c => c.Number, !request.HasOrderBy());

    // .Where(x => x.IsActive)
}

public class AdjustmentSearchRequestHandler : IRequestHandler<AdjustmentSearchRequest, PaginationResponse<AdjustmentDto>>
{
    private readonly IReadRepository<Adjustment> _repository;

    public AdjustmentSearchRequestHandler(IReadRepository<Adjustment> repository) => _repository = repository;

    public async Task<PaginationResponse<AdjustmentDto>> Handle(AdjustmentSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new AdjustmentsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}