using ZANECO.API.Application.AGMA.Winners;
using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Prizes;

public class PrizeDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public PrizeDeleteRequest(Guid id) => Id = id;
}

public class PrizeDeleteRequestHandler : IRequestHandler<PrizeDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Prize> _repoPrize;
    private readonly IReadRepository<Winner> _repoWinner;
    private readonly IStringLocalizer<PrizeDeleteRequestHandler> _localizer;

    public PrizeDeleteRequestHandler(IRepositoryWithEvents<Prize> repoPrize, IReadRepository<Winner> repoWinner, IStringLocalizer<PrizeDeleteRequestHandler> localizer) =>
        (_repoPrize, _repoWinner, _localizer) = (repoPrize, repoWinner, localizer);

    public async Task<Guid> Handle(PrizeDeleteRequest request, CancellationToken cancellationToken)
    {
        if (await _repoWinner.AnyAsync(new WinnerByPrizeSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_localizer["Raffle cannot be deleted as it's being used."]);
        }

        var prize = await _repoPrize.GetByIdAsync(request.Id, cancellationToken);
        _ = prize ?? throw new NotFoundException($"Prize {request.Id} not found.");

        await _repoPrize.DeleteAsync(prize, cancellationToken);

        return request.Id;
    }
}