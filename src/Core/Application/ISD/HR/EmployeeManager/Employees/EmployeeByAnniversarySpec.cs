using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeByAnniversarySpec : Specification<Employee, EmployeeDto>
{
    public EmployeeByAnniversarySpec() =>
        Query.Where(p => p.IsActive
                        && p.HireDate.Month.Equals(DateTime.Today.Month));

    //&& p.HireDate.Day.Equals(DateTime.Today.Day));
}