using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Raffles;

public class RaffleGetRequest : IRequest<RaffleDto>
{
    public DefaultIdType Id { get; set; }

    public RaffleGetRequest(Guid id) => Id = id;
}

public class RaffleGetRequestHandler : IRequestHandler<RaffleGetRequest, RaffleDto>
{
    private readonly IRepositoryWithEvents<Raffle> _repository;
    private readonly IStringLocalizer<RaffleGetRequestHandler> _localizer;

    public RaffleGetRequestHandler(IRepositoryWithEvents<Raffle> repository, IStringLocalizer<RaffleGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<RaffleDto> Handle(RaffleGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Raffle, RaffleDto>)new RaffleByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Raffle not found."], request.Id));
}