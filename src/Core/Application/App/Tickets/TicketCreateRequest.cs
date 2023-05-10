using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Tickets;

public class TicketCreateRequest : IRequest<Guid>
{
    public Guid GroupId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public ImageUploadRequest? Image { get; set; }
}

public class TicketCreateRequestValidator : CustomValidator<TicketCreateRequest>
{
    public TicketCreateRequestValidator(IReadRepository<Group> repoTicket, IStringLocalizer<TicketCreateRequestValidator> localizer)
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

public class TicketCreateRequestHandler : IRequestHandler<TicketCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<Ticket> _repository;
    private readonly IFileStorageService _file;

    public TicketCreateRequestHandler(IRepositoryWithEvents<Ticket> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(TicketCreateRequest request, CancellationToken cancellationToken)
    {
        string imagePath = await _file.UploadAsync<Ticket>(request.Image, FileType.Image, cancellationToken);

        var ticket = new Ticket(request.GroupId, request.Name, request.Description, request.Notes, imagePath);

        await _repository.AddAsync(ticket, cancellationToken);

        return ticket.Id;
    }
}