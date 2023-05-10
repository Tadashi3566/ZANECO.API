using ZANECO.API.Application.AGMA.Prizes;
using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Raffles;

public class RaffleDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public RaffleDeleteRequest(Guid id) => Id = id;
}

public class RaffleDeleteRequestHandler : IRequestHandler<RaffleDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Raffle> _repoRaffle;
    private readonly IReadRepository<Prize> _repoPrize;
    private readonly IStringLocalizer<RaffleDeleteRequestHandler> _localizer;

    public RaffleDeleteRequestHandler(IRepositoryWithEvents<Raffle> repoRaffle, IReadRepository<Prize> repoPrize, IStringLocalizer<RaffleDeleteRequestHandler> localizer) =>
        (_repoRaffle, _repoPrize, _localizer) = (repoRaffle, repoPrize, localizer);

    public async Task<Guid> Handle(RaffleDeleteRequest request, CancellationToken cancellationToken)
    {
        if (await _repoPrize.AnyAsync(new PrizeByRaffleSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_localizer["Raffle cannot be deleted as it's being used."]);
        }

        var raffle = await _repoRaffle.GetByIdAsync(request.Id, cancellationToken);
        _ = raffle ?? throw new NotFoundException(_localizer["Raffle not found."]);

        await _repoRaffle.DeleteAsync(raffle, cancellationToken);

        return request.Id;
    }
}