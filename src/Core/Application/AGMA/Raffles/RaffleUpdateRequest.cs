using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Raffles;

public class RaffleUpdateRequest : BaseRequestWithImage, IRequest<Guid>
{
    public DateTime RaffleDate { get; set; }
}

public class RaffleUpdateRequestValidator : CustomValidator<RaffleUpdateRequest>
{
    public RaffleUpdateRequestValidator(IReadRepository<Raffle> repoRaffleRepo, IStringLocalizer<RaffleUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(128)
            .MustAsync(async (raffle, name, ct) =>
                    await repoRaffleRepo.FirstOrDefaultAsync(new RaffleByNameSpec(name), ct)
                        is not { } existingRaffle || existingRaffle.Id == raffle.Id)
                .WithMessage((_, name) => string.Format(localizer["Raffle already exists."], name));

        RuleFor(p => p.RaffleDate)
            .NotNull();

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class RaffleUpdateRequestHandler : IRequestHandler<RaffleUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Raffle> _repository;
    private readonly IFileStorageService _file;

    public RaffleUpdateRequestHandler(IRepositoryWithEvents<Raffle> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(RaffleUpdateRequest request, CancellationToken cancellationToken)
    {
        var raffle = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = raffle ?? throw new NotFoundException($"Raffle {request.Id} not found.");

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentImagePath = raffle.ImagePath;
            if (!string.IsNullOrEmpty(currentImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentImagePath));
            }

            raffle = raffle.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Raffle>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedRaffle = raffle.Update(request.Name, request.RaffleDate, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(updatedRaffle, cancellationToken);

        return request.Id;
    }
}