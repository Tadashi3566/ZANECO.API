using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Prizes;

public class PrizeByNameSpec : Specification<Prize>, ISingleResultSpecification
{
    public PrizeByNameSpec(string name) => Query.Where(q => q.Name == name);
}