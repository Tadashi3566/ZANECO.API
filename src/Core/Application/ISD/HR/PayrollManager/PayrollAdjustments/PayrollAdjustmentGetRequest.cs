using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.PayrollAdjustments;

public class PayrollAdjustmentGetRequest : IRequest<PayrollAdjustmentDto>
{
    public DefaultIdType Id { get; set; }

    public PayrollAdjustmentGetRequest(Guid id) => Id = id;
}

public class PayrollAdjustmentGetRequestHandler : IRequestHandler<PayrollAdjustmentGetRequest, PayrollAdjustmentDto>
{
    private readonly IRepository<PayrollAdjustment> _repository;
    private readonly IStringLocalizer<PayrollAdjustmentGetRequestHandler> _localizer;

    public PayrollAdjustmentGetRequestHandler(IRepository<PayrollAdjustment> repository, IStringLocalizer<PayrollAdjustmentGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<PayrollAdjustmentDto> Handle(PayrollAdjustmentGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync((ISpecification<PayrollAdjustment, PayrollAdjustmentDto>)new PayrollAdjustmentByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Payroll Adjustment not found."], request.Id));
}