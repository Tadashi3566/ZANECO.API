using ZANECO.API.Application.Surveys.Rates;
using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Ratings;

public class RatingUpdateRequest : IRequest<Guid>
{
    public DefaultIdType Id { get; set; }
    public int RateNumber { get; set; } = default!;
    public string Comment { get; set; } = default!;
    public string Reference { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class RatingUpdateRequestValidator : CustomValidator<RatingUpdateRequest>
{
    public RatingUpdateRequestValidator()
    {
        RuleFor(p => p.RateNumber)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(5);

        RuleFor(p => p.Comment)
            .NotEmpty()
            .MaximumLength(1024);
    }
}

public class RatingUpdateRequestHandler : IRequestHandler<RatingUpdateRequest, Guid>
{
    private readonly IReadRepository<Rate> _repoRate;
    private readonly IRepositoryWithEvents<Rating> _repoRating;
    private readonly IStringLocalizer<RatingUpdateRequestHandler> _localizer;

    public RatingUpdateRequestHandler(IReadRepository<Rate> repoRate, IRepositoryWithEvents<Rating> repoRating, IStringLocalizer<RatingUpdateRequestHandler> localizer) =>
        (_repoRate, _repoRating, _localizer) = (repoRate, repoRating, localizer);

    public async Task<Guid> Handle(RatingUpdateRequest request, CancellationToken cancellationToken)
    {
        var rating = await _repoRating.GetByIdAsync(request.Id, cancellationToken);

        _ = rating ?? throw new NotFoundException(string.Format(_localizer["Rating not found."], request.Id));

        var rate = await _repoRate.FirstOrDefaultAsync(new RateByNumberSpec(request.RateNumber), cancellationToken);

        var updatedRating = rating.Update(request.RateNumber, rate!.Name, request.Comment, request.Reference, request.Description, request.Notes);

        await _repoRating.UpdateAsync(updatedRating, cancellationToken);

        return request.Id;
    }
}