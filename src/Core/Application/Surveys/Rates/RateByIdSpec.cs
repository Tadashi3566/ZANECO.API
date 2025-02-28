﻿using ZANECO.API.Domain.Surveys;

namespace ZANECO.API.Application.Surveys.Rates;

public class RateByIdSpec : Specification<Rate, RateDto>, ISingleResultSpecification<Rate>
{
    public RateByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}