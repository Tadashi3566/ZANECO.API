using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;

public class AdjustmentByNumberSpec : Specification<Adjustment>, ISingleResultSpecification<Adjustment>
{
    public AdjustmentByNumberSpec(int number) =>
        Query.Where(p => p.Number.Equals(number));
}