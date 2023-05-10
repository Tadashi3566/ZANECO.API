using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employers;

public class EmployerByIdWithEmployeeSpec : Specification<Employer, EmployerDetailsDto>, ISingleResultSpecification
{
    public EmployerByIdWithEmployeeSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Employee);
}