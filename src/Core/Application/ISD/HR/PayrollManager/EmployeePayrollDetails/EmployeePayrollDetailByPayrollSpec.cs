using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrollDetails;

public class EmployeePayrollDetailByPayrollSpec : Specification<EmployeePayrollDetail>, ISingleResultSpecification<EmployeePayrollDetail>
{
    public EmployeePayrollDetailByPayrollSpec(Guid employeeId, Guid payrollId) =>
        Query.Where(p => p.EmployeeId.Equals(employeeId) && p.PayrollId.Equals(payrollId));
}