using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.Payrolls;

public class PayrollByPayrollDetailSpec : Specification<EmployeePayrollDetail>, ISingleResultSpecification
{
    public PayrollByPayrollDetailSpec(Guid id) =>
        Query.Where(p => p.PayrollId.Equals(id) && p.Contributor.Equals("EMPLOYEE"));
}