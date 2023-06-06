using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;

public class EmployeeAdjustmentSearchRequest : PaginationFilter, IRequest<PaginationResponse<EmployeeAdjustmentDto>>
{
    public Guid? EmployeeId { get; set; }
}

public class EmployeeAdjustmentsBySearchRequestSpec : EntitiesByPaginationFilterSpec<EmployeeAdjustment, EmployeeAdjustmentDto>
{
    public EmployeeAdjustmentsBySearchRequestSpec(EmployeeAdjustmentSearchRequest request)
        : base(request) =>
        Query
            .Include(x => x.Employee)
            .OrderBy(x => x.EmployeeName, !request.HasOrderBy())
            .Where(x => x.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue);
}

public class EmployeeAdjustmentSearchRequestHandler : IRequestHandler<EmployeeAdjustmentSearchRequest, PaginationResponse<EmployeeAdjustmentDto>>
{
    private readonly IReadRepository<EmployeeAdjustment> _repository;

    public EmployeeAdjustmentSearchRequestHandler(IReadRepository<EmployeeAdjustment> repository) => _repository = repository;

    public async Task<PaginationResponse<EmployeeAdjustmentDto>> Handle(EmployeeAdjustmentSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployeeAdjustmentsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}