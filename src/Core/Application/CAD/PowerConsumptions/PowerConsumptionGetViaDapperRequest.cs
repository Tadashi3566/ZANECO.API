using Mapster;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerConsumptions;

public class PowerConsumptionGetViaDapperRequest : IRequest<PowerConsumptionDto>
{
    public DefaultIdType Id { get; set; }

    public PowerConsumptionGetViaDapperRequest(Guid id) => Id = id;
}

public class PowerConsumptionGetViaDapperRequestHandler : IRequestHandler<PowerConsumptionGetViaDapperRequest, PowerConsumptionDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<PowerConsumptionGetViaDapperRequestHandler> _localizer;

    public PowerConsumptionGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<PowerConsumptionGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<PowerConsumptionDto> Handle(PowerConsumptionGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var powerConsumption = await _repository.QueryFirstOrDefaultAsync<PowerConsumption>(
        $"SELECT * FROM datazaneco.\"PowerConsumptions\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = powerConsumption ?? throw new NotFoundException($"PowerConsumption {request.Id} not found.");

        return powerConsumption.Adapt<PowerConsumptionDto>();
    }
}