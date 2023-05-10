using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Prizes;

public class PrizeSearchRequest : PaginationFilter, IRequest<PaginationResponse<PrizeDto>>
{
}

public class PrizeBySearchRequestSpec : EntitiesByPaginationFilterSpec<Prize, PrizeDto>
{
    public PrizeBySearchRequestSpec(PrizeSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Id, !request.HasOrderBy());
}

public class PrizeSearchRequestHandler : IRequestHandler<PrizeSearchRequest, PaginationResponse<PrizeDto>>
{
    private readonly IReadRepository<Prize> _repository;

    public PrizeSearchRequestHandler(IReadRepository<Prize> repository) => _repository = repository;

    public async Task<PaginationResponse<PrizeDto>> Handle(PrizeSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new PrizeBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}