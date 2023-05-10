using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Routes;

public class RouteByCodeSpec : Specification<Route>, ISingleResultSpecification
{
    public RouteByCodeSpec(string code) => Query.Where(p => p.Code == code);
}