using Mapster;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrollDetails;

public class EmployeePayrollDetailGetViaDapperRequest : IRequest<EmployeePayrollDetailDto>
{
    public DefaultIdType Id { get; set; }

    public EmployeePayrollDetailGetViaDapperRequest(Guid id) => Id = id;
}

public class EmployeePayrollDetailGetViaDapperRequestHandler : IRequestHandler<EmployeePayrollDetailGetViaDapperRequest, EmployeePayrollDetailDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<EmployeePayrollDetailGetViaDapperRequestHandler> _localizer;

    public EmployeePayrollDetailGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<EmployeePayrollDetailGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<EmployeePayrollDetailDto> Handle(EmployeePayrollDetailGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var employeePayrollDetail = await _repository.QueryFirstOrDefaultAsync<EmployeePayrollDetail>(
        $"SELECT * FROM datazaneco.\"EmployeePayrollDetail\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = employeePayrollDetail ?? throw new NotFoundException(string.Format(_localizer["EmployeePayrollDetail not found."], request.Id));

        return employeePayrollDetail.Adapt<EmployeePayrollDetailDto>();
    }
}