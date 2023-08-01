using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;

public class EmployeeAdjustmentByNameSpec : Specification<EmployeeAdjustment>, ISingleResultSpecification<EmployeeAdjustment>
{
    public EmployeeAdjustmentByNameSpec(Guid employeeId, string name) =>
        Query.Where(p => p.EmployeeId == employeeId && p.AdjustmentName == name);
}