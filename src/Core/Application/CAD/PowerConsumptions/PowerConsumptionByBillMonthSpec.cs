using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerConsumptions;

public class PowerConsumptionByBillMonthSpec : Specification<PowerConsumption>
{
    public PowerConsumptionByBillMonthSpec(string billMonth) =>
        Query.Where(p => p.BillMonth == billMonth);
}