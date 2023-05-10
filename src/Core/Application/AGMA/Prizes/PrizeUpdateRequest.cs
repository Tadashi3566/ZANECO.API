using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Prizes;

public class PrizeUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType RaffleId { get; set; } = default!;
    public string RaffleName { get; set; } = default!;
    public string PrizeType { get; set; } = default!;
    public int Winners { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class PrizeUpdateRequestValidator : CustomValidator<PrizeUpdateRequest>
{
    public PrizeUpdateRequestValidator()
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

public class PrizeUpdateRequestHandler : IRequestHandler<PrizeUpdateRequest, Guid>
{
    private readonly IReadRepository<Raffle> _repoRaffle;
    private readonly IRepositoryWithEvents<Prize> _repository;
    private readonly IStringLocalizer<PrizeUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public PrizeUpdateRequestHandler(IReadRepository<Raffle> repoRaffle, IRepositoryWithEvents<Prize> repository, IStringLocalizer<PrizeUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repoRaffle, _repository, _localizer, _file) = (repoRaffle, repository, localizer, file);

    public async Task<Guid> Handle(PrizeUpdateRequest request, CancellationToken cancellationToken)
    {
        var prize = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = prize ?? throw new NotFoundException(string.Format(_localizer["Prize not found."], request.Id));

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentImagePath = prize.ImagePath;
            if (!string.IsNullOrEmpty(currentImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentImagePath));
            }

            prize = prize.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Prize>(request.Image, FileType.Image, cancellationToken)
            : null;

        var raffle = await _repoRaffle.GetByIdAsync(request.RaffleId, cancellationToken);
        _ = raffle ?? throw new NotFoundException("Raffle Profile not found.");

        var updatedPrize = prize.Update(raffle.Name, request.PrizeType, request.Winners, request.Name, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(updatedPrize, cancellationToken);

        return request.Id;
    }
}