using Mapster;
using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;

public class AdjustmentGetViaDapperRequest : IRequest<AdjustmentDto>
{
    public DefaultIdType Id { get; set; }

    public AdjustmentGetViaDapperRequest(Guid id) => Id = id;
}

public class AdjustmentGetViaDapperRequestHandler : IRequestHandler<AdjustmentGetViaDapperRequest, AdjustmentDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<AdjustmentGetViaDapperRequestHandler> _localizer;

    public AdjustmentGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<AdjustmentGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<AdjustmentDto> Handle(AdjustmentGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var adjustment = await _repository.QueryFirstOrDefaultAsync<Adjustment>(
        $"SELECT * FROM datazaneco.\"Adjustments\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = adjustment ?? throw new NotFoundException(string.Format(_localizer["Adjustment not found."], request.Id));

        return adjustment.Adapt<AdjustmentDto>();
    }
}