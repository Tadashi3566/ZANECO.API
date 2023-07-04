using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerConsumptions;

public class PowerConsumptionGetRequest : IRequest<PowerConsumptionDto>
{
    public DefaultIdType Id { get; set; }

    public PowerConsumptionGetRequest(Guid id) => Id = id;
}

public class PowerConsumptionGetRequestHandler : IRequestHandler<PowerConsumptionGetRequest, PowerConsumptionDto>
{
    private readonly IRepository<PowerConsumption> _repository;
    private readonly IStringLocalizer<PowerConsumptionGetRequestHandler> _localizer;

    public PowerConsumptionGetRequestHandler(IRepository<PowerConsumption> repository, IStringLocalizer<PowerConsumptionGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<PowerConsumptionDto> Handle(PowerConsumptionGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new PowerConsumptionByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["PowerConsumption not found."], request.Id));
}