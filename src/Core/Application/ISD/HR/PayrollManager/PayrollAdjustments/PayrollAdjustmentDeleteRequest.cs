using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.PayrollAdjustments;

public class PayrollAdjustmentDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public PayrollAdjustmentDeleteRequest(Guid id) => Id = id;
}

public class PayrollAdjustmentDeleteRequestHandler : IRequestHandler<PayrollAdjustmentDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<PayrollAdjustment> _repository;
    private readonly IStringLocalizer<PayrollAdjustmentDeleteRequestHandler> _localizer;

    public PayrollAdjustmentDeleteRequestHandler(IRepositoryWithEvents<PayrollAdjustment> repository, IStringLocalizer<PayrollAdjustmentDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(PayrollAdjustmentDeleteRequest request, CancellationToken cancellationToken)
    {
        var payrollAdjustment = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = payrollAdjustment ?? throw new NotFoundException($"Payroll Adjustment {request.Id} not found.");

        await _repository.DeleteAsync(payrollAdjustment, cancellationToken);

        return request.Id;
    }
}