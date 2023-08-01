using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.Areas;

public class AreaByCodeSpec : Specification<Area>, ISingleResultSpecification<Area>
{
    public AreaByCodeSpec(string code) => Query.Where(p => p.Code == code);
}