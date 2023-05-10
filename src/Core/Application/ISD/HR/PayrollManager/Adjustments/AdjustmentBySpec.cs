using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Adjustments;

public class AdjustmentBySpec : Specification<Adjustment, AdjustmentDto>, ISingleResultSpecification
{
    public AdjustmentBySpec() => Query.Where(p => p.IsOptional.Equals(false) || p.IsLoan.Equals(true))
        .OrderBy(x => x.Number);
}