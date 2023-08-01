using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Prizes;

public class PrizeByIdSpec : Specification<Prize, PrizeDto>, ISingleResultSpecification<Prize>
{
    public PrizeByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}