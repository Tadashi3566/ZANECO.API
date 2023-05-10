using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateByRateSpec : Specification<Rating>
{
    public RatingTemplateByRateSpec(Guid rateId) =>
        Query.Where(p => p.RateId == rateId);
}