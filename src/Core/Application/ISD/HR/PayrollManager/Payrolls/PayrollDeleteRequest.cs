using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Payrolls;

public class PayrollDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public PayrollDeleteRequest(Guid id) => Id = id;
}

public class PayrollDeleteRequestHandler : IRequestHandler<PayrollDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Payroll> _repository;
    private readonly IStringLocalizer<PayrollDeleteRequestHandler> _localizer;

    public PayrollDeleteRequestHandler(IRepositoryWithEvents<Payroll> repository, IStringLocalizer<PayrollDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(PayrollDeleteRequest request, CancellationToken cancellationToken)
    {
        var payroll = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = payroll ?? throw new NotFoundException($"payroll {request.Id} not found.");

        await _repository.DeleteAsync(payroll, cancellationToken);

        return request.Id;
    }
}