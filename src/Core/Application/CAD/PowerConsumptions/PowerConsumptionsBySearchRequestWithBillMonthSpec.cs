using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerConsumptions;

public class PowerConsumptionsBySearchRequestWithBillMonthSpec : EntitiesByPaginationFilterSpec<PowerConsumption, PowerConsumptionDto>
{
    public PowerConsumptionsBySearchRequestWithBillMonthSpec(PowerConsumptionSearchRequest request)
        : base(request) =>
        Query

            // .Include(p => p.BillMonth)
            .OrderBy(c => c.GroupName, !request.HasOrderBy());

    // .Where(p => p.BillMonth.Equals(request.BillMonth));
}