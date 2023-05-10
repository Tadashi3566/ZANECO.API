using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Designations;

public class DesignationByEmployeeIdSpec : Specification<Designation, DesignationDetailsDto>, ISingleResultSpecification
{
    public DesignationByEmployeeIdSpec(Guid employeeId) =>
        Query.Where(p => p.EmployeeId == employeeId);
}