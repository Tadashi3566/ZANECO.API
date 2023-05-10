using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrollDetails;

public class EmployeePayrollDetailSearchRequest : PaginationFilter, IRequest<PaginationResponse<EmployeePayrollDetailDto>>
{
    public Guid? EmployeeId { get; set; }
    public Guid? PayrollId { get; set; }
}

public class EmployeePayrollDetailsBySearchRequestSpec : EntitiesByPaginationFilterSpec<EmployeePayrollDetail, EmployeePayrollDetailDto>
{
    public EmployeePayrollDetailsBySearchRequestSpec(EmployeePayrollDetailSearchRequest request)
        : base(request) =>
        Query
            .Include(x => x.Payroll)
            .Include(x => x.Employee)
            .OrderBy(x => x.AdjustmentType, !request.HasOrderBy())
            .Where(x => x.EmployeeId.Equals(request.EmployeeId!.Value) && x.PayrollId.Equals(request.PayrollId) && x.Contributor.Equals("EMPLOYEE"), request.PayrollId.HasValue);
}

public class EmployeePayrollDetailSearchRequestHandler : IRequestHandler<EmployeePayrollDetailSearchRequest, PaginationResponse<EmployeePayrollDetailDto>>
{
    private readonly IReadRepository<EmployeePayrollDetail> _repository;

    public EmployeePayrollDetailSearchRequestHandler(IReadRepository<EmployeePayrollDetail> repository) => _repository = repository;

    public async Task<PaginationResponse<EmployeePayrollDetailDto>> Handle(EmployeePayrollDetailSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployeePayrollDetailsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}