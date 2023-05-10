using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Winners;

public class WinnerByIdSpec : Specification<Winner, WinnerDto>, ISingleResultSpecification
{
    public WinnerByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}