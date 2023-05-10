using Mapster;
using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Raffles;

public class RaffleGetViaDapperRequest : IRequest<RaffleDto>
{
    public DefaultIdType Id { get; set; }

    public RaffleGetViaDapperRequest(Guid id) => Id = id;
}

public class RaffleViaDapperGetRequestHandler : IRequestHandler<RaffleGetViaDapperRequest, RaffleDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<RaffleViaDapperGetRequestHandler> _localizer;

    public RaffleViaDapperGetRequestHandler(IDapperRepository repository, IStringLocalizer<RaffleViaDapperGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<RaffleDto> Handle(RaffleGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var raffle = await _repository.QueryFirstOrDefaultAsync<Raffle>(
            $"SELECT * FROM datazaneco.\"Raffles\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = raffle ?? throw new NotFoundException(string.Format(_localizer["Raffle not found."], request.Id));

        return raffle.Adapt<RaffleDto>();
    }
}