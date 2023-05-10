using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.PayrollAdjustments;

public class PayrollAdjustmentSearchRequest : PaginationFilter, IRequest<PaginationResponse<PayrollAdjustmentDto>>
{
    public DefaultIdType PayrollId { get; set; }
}

public class PayrollAdjustmentsBySearchRequestSpec : EntitiesByPaginationFilterSpec<PayrollAdjustment, PayrollAdjustmentDto>
{
    public PayrollAdjustmentsBySearchRequestSpec(PayrollAdjustmentSearchRequest request)
        : base(request) =>
        Query.Where(x => x.PayrollId == request.PayrollId)
             .OrderBy(c => c.AdjustmentNumber, !request.HasOrderBy());
}

public class PayrollAdjustmentSearchRequestHandler : IRequestHandler<PayrollAdjustmentSearchRequest, PaginationResponse<PayrollAdjustmentDto>>
{
    private readonly IReadRepository<PayrollAdjustment> _repository;

    public PayrollAdjustmentSearchRequestHandler(IReadRepository<PayrollAdjustment> repository) => _repository = repository;

    public async Task<PaginationResponse<PayrollAdjustmentDto>> Handle(PayrollAdjustmentSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new PayrollAdjustmentsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}