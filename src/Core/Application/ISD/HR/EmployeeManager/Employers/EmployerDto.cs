namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employers;

public class EmployerDto : DtoExtension, IDto
{
    public DefaultIdType EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public string Address { get; set; } = default!;
    public string Designation { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}