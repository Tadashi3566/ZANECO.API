using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Raffles;

public class RaffleSearchRequest : PaginationFilter, IRequest<PaginationResponse<RaffleDto>>
{
}

public class RaffleBySearchRequestSpec : EntitiesByPaginationFilterSpec<Raffle, RaffleDto>
{
    public RaffleBySearchRequestSpec(RaffleSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Id, !request.HasOrderBy());
}

public class RaffleSearchRequestHandler : IRequestHandler<RaffleSearchRequest, PaginationResponse<RaffleDto>>
{
    private readonly IReadRepository<Raffle> _repository;

    public RaffleSearchRequestHandler(IReadRepository<Raffle> repository) => _repository = repository;

    public async Task<PaginationResponse<RaffleDto>> Handle(RaffleSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new RaffleBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}