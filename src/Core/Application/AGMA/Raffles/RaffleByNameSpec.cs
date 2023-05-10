using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Raffles;

public class RaffleByNameSpec : Specification<Raffle>, ISingleResultSpecification
{
    public RaffleByNameSpec(string name) => Query.Where(q => q.Name == name);
}