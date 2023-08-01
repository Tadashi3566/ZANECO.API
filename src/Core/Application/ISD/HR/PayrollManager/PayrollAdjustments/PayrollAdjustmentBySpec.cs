using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.PayrollAdjustments;

public class PayrollAdjustmentBySpec : Specification<PayrollAdjustment, PayrollAdjustmentDto>, ISingleResultSpecification<PayrollAdjustment>
{
    public PayrollAdjustmentBySpec(Guid payrollId, Guid adjustmentId) =>
        Query.Where(p => p.PayrollId == payrollId
                                && p.AdjustmentId == adjustmentId);
}