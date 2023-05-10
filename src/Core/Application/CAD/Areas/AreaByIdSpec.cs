using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Areas;

public class AreaByIdSpec : Specification<Area, AreaDto>, ISingleResultSpecification
{
    public AreaByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}