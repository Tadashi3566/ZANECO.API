using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerRates;

public class PowerRateGetRequest : IRequest<PowerRateDto>
{
    public DefaultIdType Id { get; set; }

    public PowerRateGetRequest(Guid id) => Id = id;
}

public class PowerRateGetRequestHandler : IRequestHandler<PowerRateGetRequest, PowerRateDto>
{
    private readonly IRepository<PowerRate> _repository;
    private readonly IStringLocalizer<PowerRateGetRequestHandler> _localizer;

    public PowerRateGetRequestHandler(IRepository<PowerRate> repository, IStringLocalizer<PowerRateGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<PowerRateDto> Handle(PowerRateGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<PowerRate, PowerRateDto>)new PowerRateByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["PowerRate not found."], request.Id));
}