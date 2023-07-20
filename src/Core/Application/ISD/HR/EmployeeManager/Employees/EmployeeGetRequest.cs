using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeGetRequest : IRequest<EmployeeDto>
{
    public DefaultIdType Id { get; set; }

    public EmployeeGetRequest(Guid id) => Id = id;
}

public class GetEmployeeRequestHandler : IRequestHandler<EmployeeGetRequest, EmployeeDto>
{
    private readonly IRepository<Employee> _repository;
    private readonly IStringLocalizer<GetEmployeeRequestHandler> _localizer;

    public GetEmployeeRequestHandler(IRepository<Employee> repository, IStringLocalizer<GetEmployeeRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<EmployeeDto> Handle(EmployeeGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new EmployeeByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException($"employee {request.Id} not found.");
}