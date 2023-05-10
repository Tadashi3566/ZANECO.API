using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Ratings;

public class RatingSearchRequest : PaginationFilter, IRequest<PaginationResponse<RatingDto>>
{
}

public class RatingsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Rating, RatingDto>
{
    public RatingsBySearchRequestSpec(RatingSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Id, !request.HasOrderBy());
}

public class RatingSearchRequestHandler : IRequestHandler<RatingSearchRequest, PaginationResponse<RatingDto>>
{
    private readonly IReadRepository<Rating> _repository;

    public RatingSearchRequestHandler(IReadRepository<Rating> repository) => _repository = repository;

    public async Task<PaginationResponse<RatingDto>> Handle(RatingSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new RatingsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}