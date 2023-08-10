using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Prizes;

public class PrizeUpdateRequest : RequestWithImageExtension PrizeUpdateRequest>, IRequest<Guid>
{
    public DefaultIdType RaffleId { get; set; } = default!;
    public string RaffleName { get; set; } = default!;
    public string PrizeType { get; set; } = default!;
    public int Winners { get; set; } = default!;
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
    private readonly IRepositoryWithEvents<Prize> _repoPrize;
    private readonly IFileStorageService _file;

    public PrizeUpdateRequestHandler(
        IReadRepository<Raffle> repoRaffle,
        IRepositoryWithEvents<Prize> repoPrize,
        IFileStorageService file) =>
        (_repoRaffle, _repoPrize, _file) = (repoRaffle, repoPrize, file);

    public async Task<Guid> Handle(PrizeUpdateRequest request, CancellationToken cancellationToken)
    {
        var prize = await _repoPrize.GetByIdAsync(request.Id, cancellationToken);
        _ = prize ?? throw new NotFoundException($"Prize {request.Id} not found.");

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
        _ = raffle ?? throw new NotFoundException($"Raffle {request.RaffleId} not found.");

        var updatedPrize = prize.Update(raffle.Name, request.PrizeType, request.Winners, request.Name, request.Description, request.Notes, imagePath);

        await _repoPrize.UpdateAsync(updatedPrize, cancellationToken);

        return request.Id;
    }
}