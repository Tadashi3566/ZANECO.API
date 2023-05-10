using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Designations;

public class DesignationByNameSpec : Specification<Designation>, ISingleResultSpecification
{
    public DesignationByNameSpec(string name) =>
        Query.Where(p => p.Position == name);
}