using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplatesByRateSpec : Specification<RatingTemplate>
{
    public RatingTemplatesByRateSpec(Guid RateId) =>
        Query.Where(p => p.RateId == RateId);
}