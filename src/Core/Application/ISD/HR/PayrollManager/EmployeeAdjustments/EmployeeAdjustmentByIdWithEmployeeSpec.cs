using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeeAdjustments;

public class EmployeeAdjustmentByIdWithEmployeeSpec : Specification<EmployeeAdjustment, EmployeeAdjustmentDto>, ISingleResultSpecification<EmployeeAdjustment>
{
    public EmployeeAdjustmentByIdWithEmployeeSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Employee);
}