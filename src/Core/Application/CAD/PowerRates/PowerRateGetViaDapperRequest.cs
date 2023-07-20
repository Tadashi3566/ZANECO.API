using Mapster;
using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerRates;

public class PowerRateGetViaDapperRequest : IRequest<PowerRateDto>
{
    public DefaultIdType Id { get; set; }

    public PowerRateGetViaDapperRequest(Guid id) => Id = id;
}

public class PowerRateGetViaDapperRequestHandler : IRequestHandler<PowerRateGetViaDapperRequest, PowerRateDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<PowerRateGetViaDapperRequestHandler> _localizer;

    public PowerRateGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<PowerRateGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<PowerRateDto> Handle(PowerRateGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var powerRate = await _repository.QueryFirstOrDefaultAsync<PowerRate>(
        $"SELECT * FROM datazaneco.\"PowerRates\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = powerRate ?? throw new NotFoundException($"PowerRate {request.Id} not found.");

        return powerRate.Adapt<PowerRateDto>();
    }
}