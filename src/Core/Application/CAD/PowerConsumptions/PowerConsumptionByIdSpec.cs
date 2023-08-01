using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerConsumptions;

public class PowerConsumptionByIdSpec : Specification<PowerConsumption, PowerConsumptionDto>, ISingleResultSpecification<PowerConsumption>
{
    public PowerConsumptionByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}