using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;

public class EmployeeAdjustmentDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public EmployeeAdjustmentDeleteRequest(Guid id) => Id = id;
}

public class EmployeeAdjustmentDeleteRequestHandler : IRequestHandler<EmployeeAdjustmentDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<EmployeeAdjustment> _repository;
    private readonly IStringLocalizer<EmployeeAdjustmentDeleteRequestHandler> _localizer;

    public EmployeeAdjustmentDeleteRequestHandler(IRepositoryWithEvents<EmployeeAdjustment> repository, IStringLocalizer<EmployeeAdjustmentDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(EmployeeAdjustmentDeleteRequest request, CancellationToken cancellationToken)
    {
        var employeeAdjustment = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = employeeAdjustment ?? throw new NotFoundException(_localizer["Employee Adjustment not found."]);

        await _repository.DeleteAsync(employeeAdjustment, cancellationToken);

        return request.Id;
    }
}