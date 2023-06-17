using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeMobileSearchRequest : PaginationFilter, IRequest<List<EmployeeMobileDto>>
{
    public bool IsActive { get; set; } = default!;
}

public class EmployeeByMobileSearchRequestSpec : EntitiesByBaseFilterSpec<Employee, EmployeeMobileDto>
{
    public EmployeeByMobileSearchRequestSpec(EmployeeMobileSearchRequest request)
        : base(request) =>
        Query.Where(x => x.IsActive, request.IsActive)
             .OrderBy(c => c.LastName, !request.HasOrderBy());
}

public class EmployeeMobileSearchRequestHandler : IRequestHandler<EmployeeMobileSearchRequest, List<EmployeeMobileDto>>
{
    private readonly IReadRepository<Employee> _repository;

    public EmployeeMobileSearchRequestHandler(IReadRepository<Employee> repository) => _repository = repository;

    public async Task<List<EmployeeMobileDto>> Handle(EmployeeMobileSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployeeByMobileSearchRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}