using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Ratings;

public class RatingDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public RatingDeleteRequest(Guid id) => Id = id;
}

public class RatingDeleteRequestHandler : IRequestHandler<RatingDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Rating> _repository;
    private readonly IStringLocalizer<RatingDeleteRequestHandler> _localizer;

    public RatingDeleteRequestHandler(IRepositoryWithEvents<Rating> repository, IStringLocalizer<RatingDeleteRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(RatingDeleteRequest request, CancellationToken cancellationToken)
    {
        var rating = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = rating ?? throw new NotFoundException(_localizer["Rating not found."]);

        await _repository.DeleteAsync(rating, cancellationToken);

        return request.Id;
    }
}