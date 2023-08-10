using ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Dependents;

public class DependentDetailsDto : BaseDto, IDto
{
    public EmployeeDto Employee { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public DateTime? BirthDate { get; set; }
    public string Relation { get; set; } = default!;
}