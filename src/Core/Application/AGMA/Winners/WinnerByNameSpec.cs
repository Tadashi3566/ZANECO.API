using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Winners;

public class WinnerByNameSpec : Specification<Winner>, ISingleResultSpecification<Winner>
{
    public WinnerByNameSpec(string name) => Query.Where(q => q.Name == name);
}