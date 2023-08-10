using ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employers;

public class EmployerDetailsDto : DtoExtension, IDto
{
    public EmployeeDto Employee { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Designation { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}