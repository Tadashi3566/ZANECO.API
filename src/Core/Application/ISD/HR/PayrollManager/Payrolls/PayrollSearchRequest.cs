using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Payrolls;

public class PayrollSearchRequest : PaginationFilter, IRequest<PaginationResponse<PayrollDto>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class PayrollBySearchRequestSpec : EntitiesByPaginationFilterSpec<Payroll, PayrollDto>
{
    public PayrollBySearchRequestSpec(PayrollSearchRequest request)
        : base(request) =>
        Query
        .OrderBy(x => x.PayrollDate, !request.HasOrderBy())
        .Where(x => x.PayrollDate >= request.StartDate)
        .Where(x => x.PayrollDate <= request.EndDate);
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