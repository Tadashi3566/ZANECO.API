using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;

public class EmployeeAdjustmentByIdWithEmployeeSpec : Specification<EmployeeAdjustment, EmployeeAdjustmentDto>, ISingleResultSpecification
{
    public EmployeeAdjustmentByIdWithEmployeeSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Employee);
}