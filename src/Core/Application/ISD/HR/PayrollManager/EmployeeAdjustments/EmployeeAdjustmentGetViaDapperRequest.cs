using Mapster;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;

public class EmployeeAdjustmentGetViaDapperRequest : IRequest<EmployeeAdjustmentDto>
{
    public DefaultIdType Id { get; set; }

    public EmployeeAdjustmentGetViaDapperRequest(Guid id) => Id = id;
}

public class EmployeeAdjustmentGetViaDapperRequestHandler : IRequestHandler<EmployeeAdjustmentGetViaDapperRequest, EmployeeAdjustmentDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<EmployeeAdjustmentGetViaDapperRequestHandler> _localizer;

    public EmployeeAdjustmentGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<EmployeeAdjustmentGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<EmployeeAdjustmentDto> Handle(EmployeeAdjustmentGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var employeeAdjustment = await _repository.QueryFirstOrDefaultAsync<EmployeeAdjustment>(
        $"SELECT * FROM datazaneco.\"EmployeeAdjustments\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = employeeAdjustment ?? throw new NotFoundException(string.Format(_localizer["EmployeeAdjustment not found."], request.Id));

        return employeeAdjustment.Adapt<EmployeeAdjustmentDto>();
    }
}