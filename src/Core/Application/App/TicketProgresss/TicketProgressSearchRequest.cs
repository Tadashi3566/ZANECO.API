using ZANECO.API.Domain.App;

namespace ZANECO.API.Application.App.TicketProgresss;

public class TicketProgressearchRequest : PaginationFilter, IRequest<PaginationResponse<TicketProgressDto>>
{
}

public class TicketProgressBySearchRequestSpec : EntitiesByPaginationFilterSpec<TicketProgress, TicketProgressDto>
{
    public TicketProgressBySearchRequestSpec(TicketProgressearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Id, !request.HasOrderBy());
}

public class TicketProgressearchRequestHandler : IRequestHandler<TicketProgressearchRequest, PaginationResponse<TicketProgressDto>>
{
    private readonly IReadRepository<TicketProgress> _repository;

    public TicketProgressearchRequestHandler(IReadRepository<TicketProgress> repository) => _repository = repository;

    public async Task<PaginationResponse<TicketProgressDto>> Handle(TicketProgressearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new TicketProgressBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}