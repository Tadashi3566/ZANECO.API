using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeSearchRequest : PaginationFilter, IRequest<PaginationResponse<EmployeeDto>>
{
}

public class EmployeeBySearchRequestSpec : EntitiesByPaginationFilterSpec<Employee, EmployeeDto>
{
    public EmployeeBySearchRequestSpec(EmployeeSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.LastName, !request.HasOrderBy());
}

public class EmployeeSearchRequestHandler : IRequestHandler<EmployeeSearchRequest, PaginationResponse<EmployeeDto>>
{
    private readonly IReadRepository<Employee> _repository;

    public EmployeeSearchRequestHandler(IReadRepository<Employee> repository) => _repository = repository;

    public async Task<PaginationResponse<EmployeeDto>> Handle(EmployeeSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployeeBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}