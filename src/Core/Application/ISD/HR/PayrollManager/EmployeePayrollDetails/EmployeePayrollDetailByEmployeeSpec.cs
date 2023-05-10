using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrollDetails;

public class EmployeePayrollDetailByEmployeeSpec : Specification<EmployeePayrollDetail>
{
    public EmployeePayrollDetailByEmployeeSpec(Guid EmployeeId) =>
        Query.Where(p => p.EmployeeId == EmployeeId);
}