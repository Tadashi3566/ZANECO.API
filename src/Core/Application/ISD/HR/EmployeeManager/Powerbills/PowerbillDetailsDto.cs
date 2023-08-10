using ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Powerbills;

public class PowerbillDetailsDto : BaseDto, IDto
{
    public EmployeeDto Employee { get; set; } = default!;

    public string Account { get; set; } = default!;
    public string Meter { get; set; } = default!;
    public string Address { get; set; } = default!;
}