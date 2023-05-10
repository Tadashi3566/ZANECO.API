using ZANECO.API.Application.Surveys.Ratings;
using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Rates;

public class RateDeleteRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }

    public RateDeleteRequest(Guid id) => Id = id;
}

public class RateDeleteRequestHandler : IRequestHandler<RateDeleteRequest, Guid>
{
    private readonly IRepositoryWithEvents<Rate> _repository;
    private readonly IReadRepository<Rating> _repoRating;
    private readonly IStringLocalizer<RateDeleteRequestHandler> _localizer;

    public RateDeleteRequestHandler(IRepositoryWithEvents<Rate> repository, IReadRepository<Rating> repoRating, IStringLocalizer<RateDeleteRequestHandler> localizer) =>
        (_repository, _repoRating, _localizer) = (repository, repoRating, localizer);

    public async Task<Guid> Handle(RateDeleteRequest request, CancellationToken cancellationToken)
    {
        if (await _repoRating.AnyAsync(new RatingByRateSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_localizer["Brand cannot be deleted as it's being used."]);
        }

        var rate = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = rate ?? throw new NotFoundException(_localizer["rate not found."]);

        await _repository.DeleteAsync(rate, cancellationToken);

        return request.Id;
    }
}