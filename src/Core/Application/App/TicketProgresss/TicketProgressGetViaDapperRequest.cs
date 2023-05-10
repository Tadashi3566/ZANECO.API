using Mapster;
using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressGetViaDapperRequest : IRequest<TicketProgressDto>
{
    public DefaultIdType Id { get; set; }

    public TicketProgressGetViaDapperRequest(Guid id) => Id = id;
}

public class TicketProgressGetViaDapperRequestHandler : IRequestHandler<TicketProgressGetViaDapperRequest, TicketProgressDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<TicketProgressGetViaDapperRequestHandler> _localizer;

    public TicketProgressGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<TicketProgressGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<TicketProgressDto> Handle(TicketProgressGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var ticketProgress = await _repository.QueryFirstOrDefaultAsync<TicketProgress>(
            $"SELECT * FROM datazaneco.\"TicketProgress\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = ticketProgress ?? throw new NotFoundException(string.Format(_localizer["Ticket Progress not found."], request.Id));

        return ticketProgress.Adapt<TicketProgressDto>();
    }
}