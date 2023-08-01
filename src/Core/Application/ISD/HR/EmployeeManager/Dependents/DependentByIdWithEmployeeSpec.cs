using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Dependents;

public class DependentByIdWithEmployeeSpec : Specification<Dependent, DependentDetailsDto>, ISingleResultSpecification<Dependent>
{
    public DependentByIdWithEmployeeSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Employee);
}