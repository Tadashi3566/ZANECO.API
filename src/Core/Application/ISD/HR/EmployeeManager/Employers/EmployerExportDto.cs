namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Employers;

public class EmployerExportDto : DtoExtension<EmployerExportDto>, IDto
{
    public string Address { get; set; } = default!;
    public string Designation { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}