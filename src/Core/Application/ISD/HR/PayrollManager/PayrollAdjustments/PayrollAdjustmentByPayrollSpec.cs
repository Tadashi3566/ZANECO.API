using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.PayrollAdjustments;

public class PayrollAdjustmentByPayrollSpec : Specification<PayrollAdjustment, PayrollAdjustmentDto>, ISingleResultSpecification<PayrollAdjustment>
{
    public PayrollAdjustmentByPayrollSpec(Guid payrollId) =>
        Query.Where(p => p.PayrollId == payrollId);
}