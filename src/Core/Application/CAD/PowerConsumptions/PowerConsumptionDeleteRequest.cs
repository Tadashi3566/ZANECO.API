using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerConsumptions;

public class PowerConsumptionDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public PowerConsumptionDeleteRequest(Guid id) => Id = id;
}

public class PowerConsumptionDeleteRequestHandler : IRequestHandler<PowerConsumptionDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<PowerConsumption> _repository;
    private readonly IStringLocalizer<PowerConsumptionDeleteRequestHandler> _localizer;

    public PowerConsumptionDeleteRequestHandler(IRepositoryWithEvents<PowerConsumption> repository, IStringLocalizer<PowerConsumptionDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(PowerConsumptionDeleteRequest request, CancellationToken cancellationToken)
    {
        var powerConsumption = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = powerConsumption ?? throw new NotFoundException($"PowerConsumption {request.Id} not found.");

        await _repository.DeleteAsync(powerConsumption, cancellationToken);

        return request.Id;
    }
}