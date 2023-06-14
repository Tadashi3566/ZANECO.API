using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeMobileSearchRequest : PaginationFilter, IRequest<List<EmployeeDto>>
{
    public bool IsActive { get; set; } = default!;
}

public class EmployeeByMobileSearchRequestSpec : EntitiesByBaseFilterSpec<Employee, EmployeeDto>
{
    public EmployeeByMobileSearchRequestSpec(EmployeeMobileSearchRequest request)
        : base(request) =>
        Query.Where(x => x.IsActive, request.IsActive)
             .OrderBy(c => c.LastName, !request.HasOrderBy());
}

public class EmployeeMobileSearchRequestHandler : IRequestHandler<EmployeeMobileSearchRequest, List<EmployeeDto>>
{
    private readonly IReadRepository<Employee> _repository;

    public EmployeeMobileSearchRequestHandler(IReadRepository<Employee> repository) => _repository = repository;

    public async Task<List<EmployeeDto>> Handle(EmployeeMobileSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployeeByMobileSearchRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}