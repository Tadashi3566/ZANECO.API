namespace ZANECO.API.Application.ISD.HR.EmployeeManager.JobDescriptions;

public class JobDescriptionDto : BaseDto, IDto
{
    public int Rank { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Department { get; set; } = default!;
    public string? ReportsTo { get; set; }
}