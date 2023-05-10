using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateGetRequest : IRequest<RatingTemplateDetailsDto>
{
    public DefaultIdType Id { get; set; }

    public RatingTemplateGetRequest(Guid id) => Id = id;
}

public class RatingTemplateGetRequestHandler : IRequestHandler<RatingTemplateGetRequest, RatingTemplateDetailsDto>
{
    private readonly IRepository<RatingTemplate> _repository;
    private readonly IStringLocalizer<RatingTemplateGetRequestHandler> _localizer;

    public RatingTemplateGetRequestHandler(IRepository<RatingTemplate> repository, IStringLocalizer<RatingTemplateGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<RatingTemplateDetailsDto> Handle(RatingTemplateGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<RatingTemplate, RatingTemplateDetailsDto>)new RatingTemplateByIdWithRateSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["ratingTemplate not found."], request.Id));
}