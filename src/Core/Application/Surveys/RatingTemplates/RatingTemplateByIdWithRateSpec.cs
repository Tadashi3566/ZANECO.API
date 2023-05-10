using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateByIdWithRateSpec : Specification<RatingTemplate, RatingTemplateDetailsDto>, ISingleResultSpecification
{
    public RatingTemplateByIdWithRateSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Rate);
}