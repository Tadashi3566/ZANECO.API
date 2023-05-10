using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerRates;

public class PowerRateDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public PowerRateDeleteRequest(Guid id) => Id = id;
}

public class PowerRateDeleteRequestHandler : IRequestHandler<PowerRateDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<PowerRate> _repository;
    private readonly IStringLocalizer<PowerRateDeleteRequestHandler> _localizer;

    public PowerRateDeleteRequestHandler(IRepositoryWithEvents<PowerRate> repository, IStringLocalizer<PowerRateDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(PowerRateDeleteRequest request, CancellationToken cancellationToken)
    {
        var powerRate = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = powerRate ?? throw new NotFoundException(_localizer["PowerRate not found."]);

        await _repository.DeleteAsync(powerRate, cancellationToken);

        return request.Id;
    }
}