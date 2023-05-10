using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeAnniversaryRequest : PaginationFilter, IRequest<PaginationResponse<EmployeeDto>>
{
}

public class EmployeeByAnniversaryRequestSpec : EntitiesByPaginationFilterSpec<Employee, EmployeeDto>
{
    public EmployeeByAnniversaryRequestSpec(EmployeeAnniversaryRequest request)
        : base(request) =>
        Query.Where(x => x.IsActive
                            && x.HireDate.Month.Equals(DateTime.Today.Month));

    //&& x.HireDate.Day.Equals(DateTime.Today.Day));
}

public class EmployeeAnniversaryRequestHandler : IRequestHandler<EmployeeAnniversaryRequest, PaginationResponse<EmployeeDto>>
{
    private readonly IReadRepository<Employee> _repository;

    public EmployeeAnniversaryRequestHandler(IReadRepository<Employee> repository) => _repository = repository;

    public async Task<PaginationResponse<EmployeeDto>> Handle(EmployeeAnniversaryRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployeeByAnniversaryRequestSpec(request);
        var result = await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
        return result;
    }
}