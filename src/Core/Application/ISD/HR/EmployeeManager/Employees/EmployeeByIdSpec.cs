using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeByIdSpec : Specification<Employee, EmployeeDto>, ISingleResultSpecification<Employee>
{
    public EmployeeByIdSpec(Guid id) => Query.Where(p => p.Id == id);
}