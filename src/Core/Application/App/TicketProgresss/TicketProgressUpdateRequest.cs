using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressUpdateRequest : RequestWithImageExtension, IRequest<Guid>
{
    public Guid TicketId { get; set; }
    public string ProgressType { get; set; } = default!;
}

public class TicketProgressUpdateRequestValidator : CustomValidator<TicketProgressUpdateRequest>
{
    public TicketProgressUpdateRequestValidator(IReadRepository<Ticket> repoTicketProgress, IStringLocalizer<TicketProgressUpdateRequestValidator> localizer)
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

public class TicketProgressUpdateRequestHandler : IRequestHandler<TicketProgressUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<TicketProgress> _repository;
    private readonly IStringLocalizer<TicketProgressUpdateRequestHandler> _localizer;
    private readonly IFileStorageService _file;

    public TicketProgressUpdateRequestHandler(IRepositoryWithEvents<TicketProgress> repository, IStringLocalizer<TicketProgressUpdateRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _localizer, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(TicketProgressUpdateRequest request, CancellationToken cancellationToken)
    {
        var ticketProgress = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = ticketProgress ?? throw new NotFoundException($"Ticket Progress {request.Id} not found.");

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentTicketProgressImagePath = ticketProgress.ImagePath;
            if (!string.IsNullOrEmpty(currentTicketProgressImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentTicketProgressImagePath));
            }

            ticketProgress = ticketProgress.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<TicketProgress>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedTicketProgress = ticketProgress.Update(request.ProgressType, request.Name, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(updatedTicketProgress, cancellationToken);

        return request.Id;
    }
}