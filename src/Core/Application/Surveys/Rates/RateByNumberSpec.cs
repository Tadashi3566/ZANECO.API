using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Rates;

public class RateByNumberSpec : Specification<Rate>, ISingleResultSpecification
{
    public RateByNumberSpec(int number) =>
        Query.Where(p => p.Number.Equals(number));
}