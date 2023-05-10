using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerRates;

public class PowerRateByCodeSpec : Specification<PowerRate>, ISingleResultSpecification
{
    public PowerRateByCodeSpec(string code) => Query.Where(p => p.Code == code);
}