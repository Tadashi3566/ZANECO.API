using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrolls;

public class EmployeePayrollByIdWithEmployeeSpec : Specification<EmployeePayroll, EmployeePayrollDto>, ISingleResultSpecification<EmployeePayroll>
{
    public EmployeePayrollByIdWithEmployeeSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Employee);
}