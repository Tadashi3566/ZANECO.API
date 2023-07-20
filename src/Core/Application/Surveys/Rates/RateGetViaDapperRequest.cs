using Mapster;
using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Rates;

public class RateGetViaDapperRequest : IRequest<RateDto>
{
    public DefaultIdType Id { get; set; }

    public RateGetViaDapperRequest(Guid id) => Id = id;
}

public class RateGetViaDapperRequestHandler : IRequestHandler<RateGetViaDapperRequest, RateDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<RateGetViaDapperRequestHandler> _localizer;

    public RateGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<RateGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<RateDto> Handle(RateGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var rate = await _repository.QueryFirstOrDefaultAsync<Rate>(
            $"SELECT * FROM datazaneco.\"Rates\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = rate ?? throw new NotFoundException($"rate {request.Id} not found.");

        return rate.Adapt<RateDto>();
    }
}