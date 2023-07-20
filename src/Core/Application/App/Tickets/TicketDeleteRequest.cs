using ZANECO.API.Application.App.TicketProgresss;
using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Tickets;

public class TicketDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public TicketDeleteRequest(Guid id) => Id = id;
}

public class TicketDeleteRequestHandler : IRequestHandler<TicketDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Ticket> _repoTicket;
    private readonly IReadRepository<TicketProgress> _repoProgress;
    private readonly IStringLocalizer<TicketDeleteRequestHandler> _localizer;

    public TicketDeleteRequestHandler(IRepositoryWithEvents<Ticket> repository, IReadRepository<TicketProgress> repoProgress, IStringLocalizer<TicketDeleteRequestHandler> localizer) =>
        (_repoTicket, _repoProgress, _localizer) = (repository, repoProgress, localizer);

    public async Task<Guid> Handle(TicketDeleteRequest request, CancellationToken cancellationToken)
    {
        if (await _repoProgress.AnyAsync(new TicketProgressByTicketSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_localizer["Prize cannot be deleted as it's being used."]);
        }

        var ticket = await _repoTicket.GetByIdAsync(request.Id, cancellationToken);

        _ = ticket ?? throw new NotFoundException($"ticket {request.Id} not found.");

        await _repoTicket.DeleteAsync(ticket, cancellationToken);

        return request.Id;
    }
}