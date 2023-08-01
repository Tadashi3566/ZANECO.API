using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Areas;

public class AreaByNumberSpec : Specification<Area>, ISingleResultSpecification<Area>
{
    public AreaByNumberSpec(int number) =>
        Query.Where(p => p.Number == number);
}