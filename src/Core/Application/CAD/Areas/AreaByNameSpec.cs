using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Areas;

public class AreaByNameSpec : Specification<Area>, ISingleResultSpecification<Area>
{
    public AreaByNameSpec(string name) => Query.Where(p => p.Name == name);
}