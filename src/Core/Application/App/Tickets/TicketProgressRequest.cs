using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Tickets;

public class TicketProgressRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public string Action { get; set; } = default!;
    public Guid ProgressBy { get; set; } = default!;
}

public class TicketOpenRequestValidator : CustomValidator<TicketProgressRequest>
{
    public TicketOpenRequestValidator(IReadRepository<Ticket> repository, IStringLocalizer<TicketOpenRequestValidator> localizer)
    {
        RuleFor(p => p.ProgressBy)
            .NotEmpty()
            .MustAsync(async (id, ct) => await repository.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => string.Format(localizer["user not found."], id));
    }
}

public class TicketOpenRequestHandler : IRequestHandler<TicketProgressRequest, Guid>
{
    private readonly IRepositoryWithEvents<Ticket> _repository;
    private readonly IStringLocalizer<TicketOpenRequestHandler> _localizer;

    public TicketOpenRequestHandler(IRepositoryWithEvents<Ticket> repository, IStringLocalizer<TicketOpenRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(TicketProgressRequest request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = ticket ?? throw new NotFoundException(string.Format(_localizer["ticket not found."], request.Id));

        Ticket opendTicket = new();

        switch (request.Action)
        {
            case "Opened":
                ticket.Open(request.ProgressBy);
                break;

            case "Closed":
                ticket.Close(request.ProgressBy);
                break;

            case "Approve":
                ticket.Approve(request.ProgressBy);
                break;

            default:
                break;
        }

        await _repository.UpdateAsync(opendTicket, cancellationToken);

        return request.Id;
    }
}