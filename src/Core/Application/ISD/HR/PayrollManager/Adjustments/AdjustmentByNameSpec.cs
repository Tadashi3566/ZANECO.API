using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;

public class AdjustmentByNameSpec : Specification<Adjustment>, ISingleResultSpecification
{
    public AdjustmentByNameSpec(string name) => Query.Where(p => p.Name == name);
}