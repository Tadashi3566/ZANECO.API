using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Raffles;

public class RaffleCreateRequest : BaseRequestWithImage, IRequest<Guid>
{
    public DateTime RaffleDate { get; set; } = default!;
}

public class RaffleCreateRequestValidator : CustomValidator<RaffleCreateRequest>
{
    public RaffleCreateRequestValidator(IReadRepository<Raffle> repoRaffle, IStringLocalizer<RaffleCreateRequestValidator> localizer)
    {
        RuleFor(p => p.Name)
             .NotEmpty()
             .MaximumLength(128)
             .MustAsync(async (name, ct) => await repoRaffle.FirstOrDefaultAsync(new RaffleByNameSpec(name), ct) is null)
             .WithMessage((_, name) => string.Format(localizer["Raffle already exists."], name));

        RuleFor(p => p.RaffleDate)
            .NotNull();

        RuleFor(p => p.Image)
             .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class RaffleCreateRequestHandler : IRequestHandler<RaffleCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Raffle> _repository;
    private readonly IFileStorageService _file;

    public RaffleCreateRequestHandler(IRepositoryWithEvents<Raffle> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(RaffleCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Raffle>(request.Image, FileType.Image, cancellationToken);

        var raffle = new Raffle(request.Name, request.RaffleDate, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(raffle, cancellationToken);

        return raffle.Id;
    }
}