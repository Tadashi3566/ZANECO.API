using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerRates;

public class PowerRateByNameSpec : Specification<PowerRate>, ISingleResultSpecification<PowerRate>
{
    public PowerRateByNameSpec(string name) => Query.Where(p => p.Name == name);
}