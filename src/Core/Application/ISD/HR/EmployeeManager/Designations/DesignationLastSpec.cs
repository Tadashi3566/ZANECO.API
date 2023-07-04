using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Designations;

public sealed class DesignationLastSpec : Specification<Designation>, ISingleResultSpecification
{
    public DesignationLastSpec(DefaultIdType employeeId) =>
        Query.Where(x => x.EmployeeId.Equals(employeeId))
            .OrderByDescending(x => x.EndDate)
            .Take(1);
}