using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressCreateRequest : IRequest<Guid>
{
    public Guid TicketId { get; set; }
    public string ProgressType { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public ImageUploadRequest? Image { get; set; }
}

public class TicketProgressCreateRequestValidator : CustomValidator<TicketProgressCreateRequest>
{
    public TicketProgressCreateRequestValidator(IReadRepository<Ticket> repoTicketProgress, IStringLocalizer<TicketProgressCreateRequestValidator> localizer)
    {
        RuleFor(p => p.TicketId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await repoTicketProgress.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["Ticket not found."], id));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class TicketProgressCreateRequestHandler : IRequestHandler<TicketProgressCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<TicketProgress> _repository;
    private readonly IFileStorageService _file;

    public TicketProgressCreateRequestHandler(IRepositoryWithEvents<TicketProgress> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(TicketProgressCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<TicketProgress>(request.Image, FileType.Image, cancellationToken);

        var ticketProgress = new TicketProgress(request.TicketId, request.ProgressType, request.Name, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(ticketProgress, cancellationToken);

        return ticketProgress.Id;
    }
}