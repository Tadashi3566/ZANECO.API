using Mapster;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.PayrollAdjustments;

public class PayrollAdjustmentGetViaDapperRequest : IRequest<PayrollAdjustmentDto>
{
    public DefaultIdType Id { get; set; }

    public PayrollAdjustmentGetViaDapperRequest(Guid id) => Id = id;
}

public class PayrollAdjustmentGetViaDapperRequestHandler : IRequestHandler<PayrollAdjustmentGetViaDapperRequest, PayrollAdjustmentDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<PayrollAdjustmentGetViaDapperRequestHandler> _localizer;

    public PayrollAdjustmentGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<PayrollAdjustmentGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<PayrollAdjustmentDto> Handle(PayrollAdjustmentGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var payrollAdjustment = await _repository.QueryFirstOrDefaultAsync<PayrollAdjustment>(
        $"SELECT * FROM datazaneco.\"PayrollAdjustments\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);
        _ = payrollAdjustment ?? throw new NotFoundException(string.Format(_localizer["Payroll Adjustment not found."], request.Id));

        return payrollAdjustment.Adapt<PayrollAdjustmentDto>();
    }
}