using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrolls;

public class EmployeePayrollSearchRequest : PaginationFilter, IRequest<PaginationResponse<EmployeePayrollDto>>
{
    public Guid? PayrollId { get; set; }
}

public class EmployeePayrollsBySearchRequestSpec : EntitiesByPaginationFilterSpec<EmployeePayroll, EmployeePayrollDto>
{
    public EmployeePayrollsBySearchRequestSpec(EmployeePayrollSearchRequest request)
        : base(request) =>
        Query
            .Include(x => x.Payroll)
            .Where(x => x.PayrollId.Equals(request.PayrollId), request.PayrollId.HasValue)
            .OrderBy(x => x.EmployeeName, !request.HasOrderBy());
}

public class EmployeePayrollSearchRequestHandler : IRequestHandler<EmployeePayrollSearchRequest, PaginationResponse<EmployeePayrollDto>>
{
    private readonly IReadRepository<EmployeePayroll> _repository;

    public EmployeePayrollSearchRequestHandler(IReadRepository<EmployeePayroll> repository) => _repository = repository;

    public async Task<PaginationResponse<EmployeePayrollDto>> Handle(EmployeePayrollSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployeePayrollsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}