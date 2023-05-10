using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Winners;

public class WinnerByPrizeSpec : Specification<Winner>
{
    public WinnerByPrizeSpec(DefaultIdType prizeId) =>
        Query.Where(p => p.PrizeId == prizeId);
}