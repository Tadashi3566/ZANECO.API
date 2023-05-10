using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerRates;

public class PowerRateByIdSpec : Specification<PowerRate, PowerRateDto>, ISingleResultSpecification
{
    public PowerRateByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}