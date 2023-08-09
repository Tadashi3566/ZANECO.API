namespace ZANECO.API.Application.ISD.HR.EmployeeManager.JobDescriptions;

public class JobDescriptionDto : IDto
{
    public DefaultIdType Id { get; set; }
    public int Rank { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string Department { get; set; } = default!;
    public string ReportsTo { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? ImagePath { get; set; }
}