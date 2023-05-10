using Mapster;
using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Tickets;

public class TicketGetViaDapperRequest : IRequest<TicketDto>
{
    public DefaultIdType Id { get; set; }

    public TicketGetViaDapperRequest(Guid id) => Id = id;
}

public class TicketGetViaDapperRequestHandler : IRequestHandler<TicketGetViaDapperRequest, TicketDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<TicketGetViaDapperRequestHandler> _localizer;

    public TicketGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<TicketGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<TicketDto> Handle(TicketGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.QueryFirstOrDefaultAsync<Ticket>(
            $"SELECT * FROM datazaneco.\"Tickets\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = ticket ?? throw new NotFoundException(string.Format(_localizer["ticket not found."], request.Id));

        return ticket.Adapt<TicketDto>();
    }
}