using Mapster;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Payrolls;

public class PayrollGetViaDapperRequest : IRequest<PayrollDto>
{
    public DefaultIdType Id { get; set; }

    public PayrollGetViaDapperRequest(Guid id) => Id = id;
}

public class PayrollGetViaDapperRequestHandler : IRequestHandler<PayrollGetViaDapperRequest, PayrollDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<PayrollGetViaDapperRequestHandler> _localizer;

    public PayrollGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<PayrollGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<PayrollDto> Handle(PayrollGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var payroll = await _repository.QueryFirstOrDefaultAsync<Payroll>($"SELECT * FROM datazaneco.\"Payroll\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = payroll ?? throw new NotFoundException(string.Format(_localizer["payroll not found."], request.Id));

        return payroll.Adapt<PayrollDto>();
    }
}