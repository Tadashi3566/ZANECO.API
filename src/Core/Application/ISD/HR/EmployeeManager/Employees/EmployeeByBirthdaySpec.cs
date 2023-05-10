using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeByBirthdaySpec : Specification<Employee, EmployeeDto>
{
    public EmployeeByBirthdaySpec() =>
        Query.Where(p => p.IsActive
                            && p.BirthDate.Month.Equals(DateTime.Today.Month)
                            && p.BirthDate.Day.Equals(DateTime.Today.Day));
}