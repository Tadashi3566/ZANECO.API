using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Tickets;

public class TicketGetRequest : IRequest<TicketDetailsDto>
{
    public DefaultIdType Id { get; set; }

    public TicketGetRequest(Guid id) => Id = id;
}

public class TicketGetRequestHandler : IRequestHandler<TicketGetRequest, TicketDetailsDto>
{
    private readonly IRepository<Ticket> _repository;
    private readonly IStringLocalizer<TicketGetRequestHandler> _localizer;

    public TicketGetRequestHandler(IRepository<Ticket> repository, IStringLocalizer<TicketGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<TicketDetailsDto> Handle(TicketGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Ticket, TicketDetailsDto>)new TicketByIdWithGroupSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["ticket not found."], request.Id));
}