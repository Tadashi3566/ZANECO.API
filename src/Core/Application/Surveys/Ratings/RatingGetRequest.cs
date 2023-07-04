using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Ratings;

public class RatingGetRequest : IRequest<RatingDto>
{
    public DefaultIdType Id { get; set; }

    public RatingGetRequest(Guid id) => Id = id;
}

public class RatingGetRequestHandler : IRequestHandler<RatingGetRequest, RatingDto>
{
    private readonly IRepository<Rating> _repository;
    private readonly IStringLocalizer<RatingGetRequestHandler> _localizer;

    public RatingGetRequestHandler(IRepository<Rating> repository, IStringLocalizer<RatingGetRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<RatingDto> Handle(RatingGetRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            new RatingByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Rating not found."], request.Id));
}