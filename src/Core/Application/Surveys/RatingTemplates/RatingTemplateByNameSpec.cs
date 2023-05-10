using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.RatingTemplates;

public class RatingTemplateByCommentSpec : Specification<RatingTemplate>, ISingleResultSpecification
{
    public RatingTemplateByCommentSpec(string comment) =>
        Query.Where(p => p.Comment == comment);
}