using ZANECO.API.Domain.ISD.HR.PayrollManager;

namespace ZANECO.API.Application.ISD.HR.PayrollManager.EmployeePayrollDetails;

public class EmployeePayrollDetailByIdWithEmployeeSpec : Specification<EmployeePayrollDetail, EmployeePayrollDetailDto>, ISingleResultSpecification
{
    public EmployeePayrollDetailByIdWithEmployeeSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Employee);
}