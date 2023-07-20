using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressGetRequest : IRequest<TicketProgressDetailsDto>
{
    public DefaultIdType Id { get; set; }

    public TicketProgressGetRequest(Guid id) => Id = id;
}

public class TicketProgressGetRequestHandler : IRequestHandler<TicketProgressGetRequest, TicketProgressDetailsDto>
{
    private readonly IRepository<TicketProgress> _repository;
    private readonly IStringLocalizer<TicketProgressGetRequestHandler> _localizer;

    public TicketProgressGetRequestHandler(IRepository<TicketProgress> repository, IStringLocalizer<TicketProgressGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<TicketProgressDetailsDto> Handle(TicketProgressGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new TicketProgressByIdWithTicketSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException($"Ticket Progress {request.Id} not found.");
}