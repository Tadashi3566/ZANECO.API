using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Ratings;

public class RatingByRateSpec : Specification<Rating>
{
    public RatingByRateSpec(Guid rateId) =>
        Query.Where(p => p.RateId == rateId);
}