using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Routes;

public class RouteByNameSpec : Specification<Route>, ISingleResultSpecification<Route>
{
    public RouteByNameSpec(string name) => Query.Where(p => p.Name == name);
}