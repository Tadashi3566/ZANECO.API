using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Prizes;

public class PrizeByRaffleSpec : Specification<Prize>
{
    public PrizeByRaffleSpec(Guid raffleId) =>
        Query.Where(p => p.RaffleId == raffleId);
}