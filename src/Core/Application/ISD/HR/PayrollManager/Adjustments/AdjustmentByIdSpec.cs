using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;

public class AdjustmentByIdSpec : Specification<Adjustment, AdjustmentDto>, ISingleResultSpecification<Adjustment>
{
    public AdjustmentByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}