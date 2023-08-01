using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Routes;

public class RouteByIdSpec : Specification<Route, RouteDto>, ISingleResultSpecification<Route>
{
    public RouteByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}