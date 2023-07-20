using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Payrolls;

public class PayrollGetRequest : IRequest<PayrollDto>
{
    public DefaultIdType Id { get; set; }

    public PayrollGetRequest(Guid id) => Id = id;
}

public class PayrollGetRequestHandler : IRequestHandler<PayrollGetRequest, PayrollDto>
{
    private readonly IRepository<Payroll> _repository;
    private readonly IStringLocalizer<PayrollGetRequestHandler> _localizer;

    public PayrollGetRequestHandler(IRepository<Payroll> repository, IStringLocalizer<PayrollGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<PayrollDto> Handle(PayrollGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(new PayrollByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException($"payroll {request.Id} not found.");
}