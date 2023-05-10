using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Ratings;

public class RatingByIdSpec : Specification<Rating, RatingDto>, ISingleResultSpecification
{
    public RatingByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}