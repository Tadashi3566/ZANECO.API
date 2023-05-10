using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Payrolls;

public class PayrollSearchRequest : PaginationFilter, IRequest<PaginationResponse<PayrollDto>>
{
}

public class PayrollBySearchRequestSpec : EntitiesByPaginationFilterSpec<Payroll, PayrollDto>
{
    public PayrollBySearchRequestSpec(PayrollSearchRequest request)
        : base(request) =>
        Query.OrderByDescending(c => c.PayrollDate, !request.HasOrderBy());
}

public class PayrollSearchRequestHandler : IRequestHandler<PayrollSearchRequest, PaginationResponse<PayrollDto>>
{
    private readonly IReadRepository<Payroll> _repository;

    public PayrollSearchRequestHandler(IReadRepository<Payroll> repository) => _repository = repository;

    public async Task<PaginationResponse<PayrollDto>> Handle(PayrollSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new PayrollBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}