using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.Tickets;

public class TicketSearchRequest : PaginationFilter, IRequest<PaginationResponse<TicketDto>>
{
}

public class TicketsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Ticket, TicketDto>
{
    public TicketsBySearchRequestSpec(TicketSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Urgency, !request.HasOrderBy());
}

public class TicketSearchRequestHandler : IRequestHandler<TicketSearchRequest, PaginationResponse<TicketDto>>
{
    private readonly IReadRepository<Ticket> _repository;

    public TicketSearchRequestHandler(IReadRepository<Ticket> repository) => _repository = repository;

    public async Task<PaginationResponse<TicketDto>> Handle(TicketSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new TicketsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}