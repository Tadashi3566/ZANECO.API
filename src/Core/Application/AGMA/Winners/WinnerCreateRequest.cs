using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Winners;

public class WinnerCreateRequest : RequestWithImageExtension<WinnerCreateRequest>, IRequest<Guid>
{
    public DefaultIdType RaffleId { get; set; } = default!;
    public string RaffleName { get; set; } = default!;
    public DefaultIdType PrizeId { get; set; } = default!;
    public string PrizeName { get; set; } = default!;
    public string Address { get; set; } = default!;
}

public class WinnerCreateRequestValidator : CustomValidator<WinnerCreateRequest>
{
    public WinnerCreateRequestValidator()
    {
        RuleFor(p => p.RaffleId)
            .NotEmpty();

        RuleFor(p => p.PrizeId)
            .NotEmpty();

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class WinnerCreateRequestHandler : IRequestHandler<WinnerCreateRequest, Guid>
{
    private readonly IReadRepository<Raffle> _repoRaffle;
    private readonly IReadRepository<Prize> _repoPrize;
    private readonly IRepositoryWithEvents<Winner> _repoWinner;
    private readonly IFileStorageService _file;

    public WinnerCreateRequestHandler(
        IReadRepository<Raffle> repoRaffle,
        IReadRepository<Prize> repoPrize,
        IRepositoryWithEvents<Winner> repoWinner,
        IFileStorageService file) =>
        (_repoRaffle, _repoPrize, _repoWinner, _file) = (repoRaffle, repoPrize, repoWinner, file);

    public async Task<Guid> Handle(WinnerCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Winner>(request.Image, FileType.Image, cancellationToken);

        var raffle = await _repoRaffle.GetByIdAsync(request.RaffleId, cancellationToken);
        _ = raffle ?? throw new NotFoundException($"Raffle {request.RaffleId} not found.");

        var prize = await _repoPrize.GetByIdAsync(request.PrizeId, cancellationToken);
        _ = prize ?? throw new NotFoundException($"Prize {request.PrizeId} not found.");

        var winner = new Winner(request.RaffleId, raffle.Name, request.PrizeId, prize.Name, request.Name, request.Address, request.Description, request.Notes, imagePath);

        await _repoWinner.AddAsync(winner, cancellationToken);

        return winner.Id;
    }
}