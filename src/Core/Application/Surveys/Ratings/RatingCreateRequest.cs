using ZANECO.API.Application.Surveys.Rates;
using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Ratings;

public class RatingCreateRequest : IRequest<DefaultIdType>
{
    public int RateNumber { get; set; } = default!;
    public string Comment { get; set; } = default!;
    public string Reference { get; set; } = default!;

    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class CreateRatingRequestValidator : CustomValidator<RatingCreateRequest>
{
    public CreateRatingRequestValidator()
    {
        RuleFor(p => p.RateNumber)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(5);
    }
}

public class RatingCreateRequestHandler : IRequestHandler<RatingCreateRequest, DefaultIdType>
{
    private readonly IReadRepository<Rate> _repoRate;
    private readonly IRepositoryWithEvents<Rating> _repository;

    public RatingCreateRequestHandler(IReadRepository<Rate> repoRate, IRepositoryWithEvents<Rating> repository) =>
        (_repoRate, _repository) = (repoRate, repository);

    public async Task<DefaultIdType> Handle(RatingCreateRequest request, CancellationToken cancellationToken)
    {
        var rate = await _repoRate.FirstOrDefaultAsync(new RateByNumberSpec(request.RateNumber), cancellationToken);

        var rating = new Rating(rate!.Id, request.RateNumber, rate!.Name, request.Comment, request.Reference, request.Description, request.Notes);

        await _repository.AddAsync(rating, cancellationToken);

        return rating.Id;
    }
}