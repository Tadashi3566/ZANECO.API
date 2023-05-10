using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Salaries;

public class SalarySearchRequest : PaginationFilter, IRequest<PaginationResponse<SalaryDto>>
{
    //public DateTime? Date { get; set; }
}

public class SalariesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Salary, SalaryDto>
{
    public SalariesBySearchRequestSpec(SalarySearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Number, !request.HasOrderBy());

    //.Where(c => c.IsActive);

    //.Where(c => c.IsActive, request.Date.HasValue)

    //Query
    //.Include(x => x.Employee)
    //.OrderBy(x => x.AttendanceDate, !request.HasOrderBy())
    //.Where(x => x.EmployeeId.Equals(request.EmployeeId!.Value), request.EmployeeId.HasValue)
    //.Where(x => x.AttendanceDate >= request.StartDate, request.StartDate.HasValue)
    //.Where(x => x.AttendanceDate <= request.EndDate, request.EndDate.HasValue);
}

public class SalarySearchRequestHandler : IRequestHandler<SalarySearchRequest, PaginationResponse<SalaryDto>>
{
    private readonly IReadRepository<Salary> _repository;

    public SalarySearchRequestHandler(IReadRepository<Salary> repository) => _repository = repository;

    public async Task<PaginationResponse<SalaryDto>> Handle(SalarySearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new SalariesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}