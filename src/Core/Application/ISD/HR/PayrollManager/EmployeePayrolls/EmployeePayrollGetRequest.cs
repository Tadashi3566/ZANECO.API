using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrolls;

public class EmployeePayrollGetRequest : IRequest<EmployeePayrollDto>
{
    public DefaultIdType Id { get; set; }

    public EmployeePayrollGetRequest(Guid id) => Id = id;
}

public class EmployeePayrollGetRequestHandler : IRequestHandler<EmployeePayrollGetRequest, EmployeePayrollDto>
{
    private readonly IRepository<EmployeePayroll> _repository;
    private readonly IStringLocalizer<EmployeePayrollGetRequestHandler> _localizer;

    public EmployeePayrollGetRequestHandler(IRepository<EmployeePayroll> repository, IStringLocalizer<EmployeePayrollGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<EmployeePayrollDto> Handle(EmployeePayrollGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new EmployeePayrollByIdWithEmployeeSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException($"EmployeePayroll {request.Id} not found.");
}