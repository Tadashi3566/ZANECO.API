using Mapster;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrolls;

public class EmployeePayrollGetViaDapperRequest : IRequest<EmployeePayrollDto>
{
    public string EmployeeId { get; set; }
    public string PayrollId { get; set; }

    public EmployeePayrollGetViaDapperRequest(string employeeId, string payrollId) => (EmployeeId, PayrollId) = (employeeId, payrollId);
}

public class EmployeePayrollGetViaDapperRequestHandler : IRequestHandler<EmployeePayrollGetViaDapperRequest, EmployeePayrollDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<EmployeePayrollGetViaDapperRequestHandler> _localizer;

    public EmployeePayrollGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<EmployeePayrollGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<EmployeePayrollDto> Handle(EmployeePayrollGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var employeePayroll = await _repository.QueryFirstOrDefaultAsync<EmployeePayroll>(
        $"SELECT * FROM datazaneco.\"EmployeePayroll\" WHERE \"EmployeeId\" = '{request.EmployeeId}' AND \"PayrollId\" = '{request.PayrollId}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = employeePayroll ?? throw new NotFoundException(string.Format(_localizer["employeePayroll not found."], request.PayrollId));

        return employeePayroll.Adapt<EmployeePayrollDto>();
    }
}