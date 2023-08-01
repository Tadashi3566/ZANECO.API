using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;

public class EmployeeAdjustmentByAdjustmentIdSpec : Specification<EmployeeAdjustment>, ISingleResultSpecification<EmployeeAdjustment>
{
    public EmployeeAdjustmentByAdjustmentIdSpec(Guid employeeId, Guid adjustmentId) =>
        Query.Where(p => p.EmployeeId == employeeId && p.AdjustmentId == adjustmentId);
}