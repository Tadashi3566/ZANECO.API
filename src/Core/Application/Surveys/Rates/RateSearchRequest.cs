using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Rates;

public class RateSearchRequest : PaginationFilter, IRequest<PaginationResponse<RateDto>>
{
}

public class RatesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Rate, RateDto>
{
    public RatesBySearchRequestSpec(RateSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Number, !request.HasOrderBy());
}

public class RateSearchRequestHandler : IRequestHandler<RateSearchRequest, PaginationResponse<RateDto>>
{
    private readonly IReadRepository<Rate> _repository;

    public RateSearchRequestHandler(IReadRepository<Rate> repository) => _repository = repository;

    public async Task<PaginationResponse<RateDto>> Handle(RateSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new RatesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}