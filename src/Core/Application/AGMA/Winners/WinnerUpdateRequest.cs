using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Winners;

public class WinnerUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType RaffleId { get; set; } = default!;
    public string RaffleName { get; set; } = default!;
    public DefaultIdType PrizeId { get; set; } = default!;
    public string PrizeName { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool DeleteCurrentImage { get; set; }
    public ImageUploadRequest? Image { get; set; }
}

public class WinnerUpdateRequestValidator : CustomValidator<WinnerUpdateRequest>
{
    public WinnerUpdateRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class WinnerUpdateRequestHandler : IRequestHandler<WinnerUpdateRequest, Guid>
{
    private readonly IReadRepository<Raffle> _repoRaffle;
    private readonly IReadRepository<Prize> _repoPrize;
    private readonly IRepositoryWithEvents<Winner> _repository;
    private readonly IFileStorageService _file;

    public WinnerUpdateRequestHandler(
        IReadRepository<Raffle> repoRaffle,
        IReadRepository<Prize> repoPrize,
        IRepositoryWithEvents<Winner> repository,
        IFileStorageService file) =>
        (_repoRaffle, _repoPrize, _repository, _file) = (repoRaffle, repoPrize, repository, file);

    public async Task<Guid> Handle(WinnerUpdateRequest request, CancellationToken cancellationToken)
    {
        var winner = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = winner ?? throw new NotFoundException("Winner not found.");

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentImagePath = winner.ImagePath;
            if (!string.IsNullOrEmpty(currentImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentImagePath));
            }

            winner = winner.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Winner>(request.Image, FileType.Image, cancellationToken)
            : null;

        var raffle = await _repoRaffle.GetByIdAsync(request.RaffleId, cancellationToken);
        _ = raffle ?? throw new NotFoundException("Raffle Profile not found.");

        var prize = await _repoPrize.GetByIdAsync(request.PrizeId, cancellationToken);
        _ = prize ?? throw new NotFoundException("Raffle Prize not found.");

        var updatedWinner = winner.Update(raffle.Name, prize.Name, request.Name, request.Address, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(updatedWinner, cancellationToken);

        return request.Id;
    }
}