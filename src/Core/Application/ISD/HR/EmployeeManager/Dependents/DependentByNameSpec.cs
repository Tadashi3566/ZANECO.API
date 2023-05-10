using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Dependents;

public class DependentByNameSpec : Specification<Dependent>, ISingleResultSpecification
{
    public DependentByNameSpec(string name) =>
        Query.Where(p => p.Name == name);
}