using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Prizes;

public class PrizeByNameSpec : Specification<Prize>, ISingleResultSpecification<Prize>
{
    public PrizeByNameSpec(string name) => Query.Where(q => q.Name == name);
}