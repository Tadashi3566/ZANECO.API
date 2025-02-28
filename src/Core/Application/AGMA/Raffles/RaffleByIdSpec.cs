﻿using ZANECO.API.Domain.AGMA;

namespace ZANECO.API.Application.AGMA.Raffles;

public class RaffleByIdSpec : Specification<Raffle, RaffleDto>, ISingleResultSpecification<Raffle>
{
    public RaffleByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}