using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.PayrollAdjustments;

public class PayrollAdjustmentByIdSpec : Specification<PayrollAdjustment, PayrollAdjustmentDto>, ISingleResultSpecification
{
    public PayrollAdjustmentByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}