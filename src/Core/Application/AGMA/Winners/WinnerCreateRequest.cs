using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Winners;

public class WinnerCreateRequest : IRequest<Guid>
{
    public DefaultIdType RaffleId { get; set; } = default!;
    public string RaffleName { get; set; } = default!;
    public DefaultIdType PrizeId { get; set; } = default!;
    public string PrizeName { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public ImageUploadRequest? Image { get; set; }
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
    private readonly IRepositoryWithEvents<Winner> _repository;
    private readonly IFileStorageService _file;

    public WinnerCreateRequestHandler(
        IReadRepository<Raffle> repoRaffle,
        IReadRepository<Prize> repoPrize,
        IRepositoryWithEvents<Winner> repository,
        IFileStorageService file) =>
        (_repoRaffle, _repoPrize, _repository, _file) = (repoRaffle, repoPrize, repository, file);

    public async Task<Guid> Handle(WinnerCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Winner>(request.Image, FileType.Image, cancellationToken);

        var raffle = await _repoRaffle.GetByIdAsync(request.RaffleId, cancellationToken);
        _ = raffle ?? throw new NotFoundException("Raffle Profile not found.");

        var prize = await _repoPrize.GetByIdAsync(request.PrizeId, cancellationToken);
        _ = prize ?? throw new NotFoundException("Raffle Prize not found.");

        var winner = new Winner(request.RaffleId, raffle.Name, request.PrizeId, prize.Name, request.Name, request.Address, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(winner, cancellationToken);

        return winner.Id;
    }
}