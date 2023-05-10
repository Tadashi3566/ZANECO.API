using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrolls;

public class EmployeePayrollBySpec : Specification<EmployeePayroll>, ISingleResultSpecification
{
    public EmployeePayrollBySpec(Guid employeeId, Guid payrollId) =>
        Query.Where(p => p.EmployeeId == employeeId && p.PayrollId == payrollId);
}