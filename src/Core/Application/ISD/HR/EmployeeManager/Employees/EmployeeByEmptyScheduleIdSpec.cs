using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

public class EmployeeByEmptyScheduleIdSpec : Specification<Employee, EmployeeDto>, ISingleResultSpecification<Employee>
{
    public EmployeeByEmptyScheduleIdSpec() => Query.Where(p => p.ScheduleId == Guid.Empty);
}