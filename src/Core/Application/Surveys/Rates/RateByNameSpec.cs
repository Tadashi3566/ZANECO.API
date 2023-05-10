using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Rates;

public class RateByNameSpec : Specification<Rate>, ISingleResultSpecification
{
    public RateByNameSpec(string name) =>
        Query.Where(p => p.Name == name);
}