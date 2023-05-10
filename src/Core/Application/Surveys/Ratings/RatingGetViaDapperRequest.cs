using Mapster;
using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Ratings;

public class RatingGetViaDapperRequest : IRequest<RatingDto>
{
    public DefaultIdType Id { get; set; }

    public RatingGetViaDapperRequest(Guid id) => Id = id;
}

public class RatingGetViaDapperRequestHandler : IRequestHandler<RatingGetViaDapperRequest, RatingDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<RatingGetViaDapperRequestHandler> _localizer;

    public RatingGetViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<RatingGetViaDapperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<RatingDto> Handle(RatingGetViaDapperRequest request, CancellationToken cancellationToken)
    {
        var rating = await _repository.QueryFirstOrDefaultAsync<Rating>(
            $"SELECT * FROM datazaneco.\"Ratings\" WHERE \"Id\" = '{request.Id}' AND \"TenantId\" = '@tenant'", cancellationToken: cancellationToken);

        _ = rating ?? throw new NotFoundException(string.Format(_localizer["Rating not found."], request.Id));

        return rating.Adapt<RatingDto>();
    }
}