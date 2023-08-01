using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;
public class EmployeeByNumberSpec : Specification<Employee>, ISingleResultSpecification<Employee>
{
    public EmployeeByNumberSpec(int number) =>
        Query.Where(p => p.Number.Equals(number));
}