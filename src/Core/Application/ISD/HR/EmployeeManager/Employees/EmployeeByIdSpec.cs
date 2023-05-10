using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeByIdSpec : Specification<Employee, EmployeeDto>, ISingleResultSpecification
{
    public EmployeeByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}