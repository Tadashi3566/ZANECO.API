using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerConsumptions;

public class PowerConsumptionByNameSpec : Specification<PowerConsumption, PowerConsumptionDto>, ISingleResultSpecification
{
    public PowerConsumptionByNameSpec(string name) => Query.Where(p => p.GroupName == name);
}