using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateSearchRequest : PaginationFilter, IRequest<PaginationResponse<RatingTemplateDto>>
{
}

public class RatingTemplatesBySearchRequestSpec : EntitiesByPaginationFilterSpec<RatingTemplate, RatingTemplateDto>
{
    public RatingTemplatesBySearchRequestSpec(RatingTemplateSearchRequest request)
        : base(request) =>
        Query.OrderBy(c => c.RateId, !request.HasOrderBy());
}

public class RatingTemplateSearchRequestHandler : IRequestHandler<RatingTemplateSearchRequest, PaginationResponse<RatingTemplateDto>>
{
    private readonly IReadRepository<RatingTemplate> _repository;

    public RatingTemplateSearchRequestHandler(IReadRepository<RatingTemplate> repository) => _repository = repository;

    public async Task<PaginationResponse<RatingTemplateDto>> Handle(RatingTemplateSearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new RatingTemplatesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}