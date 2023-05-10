using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeBirthdayRequest : PaginationFilter, IRequest<PaginationResponse<EmployeeDto>>
{
}

public class EmployeeByBirthdayRequestSpec : EntitiesByPaginationFilterSpec<Employee, EmployeeDto>
{
    public EmployeeByBirthdayRequestSpec(EmployeeBirthdayRequest request)
        : base(request) =>
        Query.Where(x => x.IsActive
                            && x.BirthDate.Month.Equals(DateTime.Today.Month));

    //&& x.BirthDate.Day.Equals(DateTime.Today.Day));
}

public class EmployeeBirthdayRequestHandler : IRequestHandler<EmployeeBirthdayRequest, PaginationResponse<EmployeeDto>>
{
    private readonly IReadRepository<Employee> _repository;

    public EmployeeBirthdayRequestHandler(IReadRepository<Employee> repository) => _repository = repository;

    public async Task<PaginationResponse<EmployeeDto>> Handle(EmployeeBirthdayRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployeeByBirthdayRequestSpec(request);
        var result = await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);

        return result;
    }
}