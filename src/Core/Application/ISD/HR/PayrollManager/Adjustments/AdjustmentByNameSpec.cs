using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;

public class AdjustmentByNameSpec : Specification<Adjustment>, ISingleResultSpecification<Adjustment>
{
    public AdjustmentByNameSpec(string name) => Query.Where(p => p.Name == name);
}