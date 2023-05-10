using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrollDetails;

public class EmployeePayrollDetailDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public EmployeePayrollDetailDeleteRequest(Guid id) => Id = id;
}

public class EmployeePayrollDetailDeleteRequestHandler : IRequestHandler<EmployeePayrollDetailDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<EmployeePayrollDetail> _repository;
    private readonly IStringLocalizer<EmployeePayrollDetailDeleteRequestHandler> _localizer;

    public EmployeePayrollDetailDeleteRequestHandler(IRepositoryWithEvents<EmployeePayrollDetail> repository, IStringLocalizer<EmployeePayrollDetailDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(EmployeePayrollDetailDeleteRequest request, CancellationToken cancellationToken)
    {
        var employeePayrollDetail = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = employeePayrollDetail ?? throw new NotFoundException(_localizer["EmployeePayrollDetail not found."]);

        await _repository.DeleteAsync(employeePayrollDetail, cancellationToken);

        return request.Id;
    }
}