using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;

public class EmployeeAdjustmentByEmployeeSpec : Specification<EmployeeAdjustment>
{
    public EmployeeAdjustmentByEmployeeSpec(Guid EmployeeId) =>
        Query.Where(p => p.EmployeeId == EmployeeId);
}