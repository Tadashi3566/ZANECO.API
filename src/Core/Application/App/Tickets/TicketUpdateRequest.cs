using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Tickets;

public class TicketUpdateRequest : RequestWithImageExtension<TicketUpdateRequest>, IRequest<Guid>
{
    public Guid GroupId { get; set; }
    public string? Impact { get; set; }
    public string? Urgency { get; set; }
    public string? Priority { get; set; }
    public string? RequestedBy { get; set; }
    public string? RequestThrough { get; set; }
    public string? Reference { get; set; }
    public string? AssignedTo { get; set; }

}

public class TicketUpdateRequestValidator : CustomValidator<TicketUpdateRequest>
{
    public TicketUpdateRequestValidator(IReadRepository<Group> repoTicket, IStringLocalizer<TicketUpdateRequestValidator> localizer)
    {
        RuleFor(p => p.GroupId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await repoTicket.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["group not found."], id));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024);

        RuleFor(p => p.Image)
            .SetNonNullableValidator(new ImageUploadRequestValidator());
    }
}

public class TicketUpdateRequestHandler : IRequestHandler<TicketUpdateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Ticket> _repository;
    private readonly IFileStorageService _file;

    public TicketUpdateRequestHandler(IRepositoryWithEvents<Ticket> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(TicketUpdateRequest request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = ticket ?? throw new NotFoundException($"ticket {request.Id} not found.");

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentTicketImagePath = ticket.ImagePath;
            if (!string.IsNullOrEmpty(currentTicketImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentTicketImagePath));
            }

            ticket = ticket.ClearImagePath();
        }

        string? imagePath = request.Image is not null
            ? await _file.UploadAsync<Ticket>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedTicket = ticket.Update(request.GroupId, request.Name, request.Impact, request.Urgency, request.Priority, request.RequestedBy, request.RequestThrough, request.Reference, request.AssignedTo, request.Description, request.Notes, imagePath);

        await _repository.UpdateAsync(updatedTicket, cancellationToken);

        return request.Id;
    }
}