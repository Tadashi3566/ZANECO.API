namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Dependents;

public class DependentExportDto : DtoExtension<DependentExportDto>, IDto
{
    public string Gender { get; set; } = default!;
    public DateTime? BirthDate { get; set; }
    public string Relation { get; set; } = default!;
}