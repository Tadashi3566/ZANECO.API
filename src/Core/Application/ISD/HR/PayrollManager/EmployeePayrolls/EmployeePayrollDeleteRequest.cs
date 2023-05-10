using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrolls;

public class EmployeePayrollDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public EmployeePayrollDeleteRequest(Guid id) => Id = id;
}

public class EmployeePayrollDeleteRequestHandler : IRequestHandler<EmployeePayrollDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<EmployeePayroll> _repository;
    private readonly IStringLocalizer<EmployeePayrollDeleteRequestHandler> _localizer;

    public EmployeePayrollDeleteRequestHandler(IRepositoryWithEvents<EmployeePayroll> repository, IStringLocalizer<EmployeePayrollDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(EmployeePayrollDeleteRequest request, CancellationToken cancellationToken)
    {
        var employeePayroll = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = employeePayroll ?? throw new NotFoundException(_localizer["Employee Payroll not found."]);

        await _repository.DeleteAsync(employeePayroll, cancellationToken);

        return request.Id;
    }
}