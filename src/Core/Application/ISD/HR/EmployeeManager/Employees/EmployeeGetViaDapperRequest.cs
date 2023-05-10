using Mapster;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeGetViaDapperRequest : IRequest<EmployeeDto>
{
    public DefaultIdType Id { get; set; }

    public EmployeeGetViaDapperRequest(Guid id) => Id = id;
}

public class EmployeeGetViaDapperRequestHandler : IRequestHandler<EmployeeGetViaDapperRequest, EmployeeDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<EmployeeGetViaDapperRequestHandler> _localizer;

    public EmployeeGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<EmployeeGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<EmployeeDto> Handle(EmployeeGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var employee = await _repository.QueryFirstOrDefaultAsync<Employee>(
        $"SELECT * FROM datazaneco.\"Employees\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = employee ?? throw new NotFoundException(string.Format(_localizer["employee not found."], request.Id));

        return employee.Adapt<EmployeeDto>();
    }
}