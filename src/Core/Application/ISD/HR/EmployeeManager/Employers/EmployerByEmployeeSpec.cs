using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employers;

public class EmployerByEmployeeSpec : Specification<Employer>
{
    public EmployerByEmployeeSpec(Guid EmployeeId) =>
        Query.Where(p => p.EmployeeId == EmployeeId);
}