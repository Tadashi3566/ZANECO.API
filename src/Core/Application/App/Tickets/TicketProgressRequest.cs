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

    public TicketOpenRequestHandler(IRepositoryWithEvents<Ticket> repository) =>
        (_repository) = (repository);

    public async Task<Guid> Handle(TicketProgressRequest request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = ticket ?? throw new NotFoundException($"ticket {request.Id} not found.");

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