using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Prizes;

public class PrizeCreateRequest : IRequest<Guid>
{
    public DefaultIdType RaffleId { get; set; } = default!;
    public string PrizeType { get; set; } = default!;
    public int Winners { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public ImageUploadRequest? Image { get; set; }
}

public class PrizeCreateRequestValidator : CustomValidator<PrizeCreateRequest>
{
    public PrizeCreateRequestValidator()
    {
        RuleFor(p => p.RaffleId)
            .NotEmpty();

        RuleFor(p => p.PrizeType)
            .NotEmpty()
            .MaximumLength(16);

        RuleFor(p => p.Winners)
            .GreaterThan(0);

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(128);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class PrizeCreateRequestHandler : IRequestHandler<PrizeCreateRequest, Guid>
{
    private readonly IReadRepository<Raffle> _repoRaffle;
    private readonly IRepositoryWithEvents<Prize> _repository;
    private readonly IFileStorageService _file;

    public PrizeCreateRequestHandler(IReadRepository<Raffle> repoRaffle, IRepositoryWithEvents<Prize> repository, IFileStorageService file) =>
        (_repoRaffle, _repository, _file) = (repoRaffle, repository, file);

    public async Task<Guid> Handle(PrizeCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Prize>(request.Image, FileType.Image, cancellationToken);

        var raffle = await _repoRaffle.GetByIdAsync(request.RaffleId, cancellationToken);
        _ = raffle ?? throw new NotFoundException("Raffle Profile not found.");

        var prize = new Prize(request.RaffleId, raffle.Name, request.PrizeType, request.Winners, request.Name, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(prize, cancellationToken);

        return prize.Id;
    }
}