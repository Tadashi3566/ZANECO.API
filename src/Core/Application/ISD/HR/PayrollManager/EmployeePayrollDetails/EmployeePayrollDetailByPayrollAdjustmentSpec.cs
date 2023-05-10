using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrollDetails;

public class EmployeePayrollDetailByPayrollAdjustmentSpec : Specification<EmployeePayrollDetail>, ISingleResultSpecification
{
    public EmployeePayrollDetailByPayrollAdjustmentSpec(Guid employeeId, Guid payrollId, Guid adjustmentId, string contributor) =>
        Query.Where(p =>
            p.EmployeeId.Equals(employeeId)
            && p.PayrollId.Equals(payrollId)
            && p.AdjustmentId.Equals(adjustmentId)
            && p.Contributor.Equals(contributor));
}