using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Dependents;

public class DependentByEmployeeSpec : Specification<Dependent>
{
    public DependentByEmployeeSpec(Guid EmployeeId) =>
        Query.Where(p => p.EmployeeId == EmployeeId);
}