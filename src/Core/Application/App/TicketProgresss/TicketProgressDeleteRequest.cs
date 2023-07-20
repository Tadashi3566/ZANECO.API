using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public TicketProgressDeleteRequest(Guid id) => Id = id;
}

public class TicketProgressDeleteRequestHandler : IRequestHandler<TicketProgressDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<TicketProgress> _repository;
    private readonly IStringLocalizer<TicketProgressDeleteRequestHandler> _localizer;

    public TicketProgressDeleteRequestHandler(IRepositoryWithEvents<TicketProgress> repository, IStringLocalizer<TicketProgressDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(TicketProgressDeleteRequest request, CancellationToken cancellationToken)
    {
        var ticketProgress = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = ticketProgress ?? throw new NotFoundException($"Ticket Progress {request.Id} not found.");

        await _repository.DeleteAsync(ticketProgress, cancellationToken);

        return request.Id;
    }
}