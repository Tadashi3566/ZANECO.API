using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Routes;

public class RouteByNumberSpec : Specification<Route, RouteDto>, ISingleResultSpecification
{
    public RouteByNumberSpec(int number) => Query.Where(p => p.Number == number);
}