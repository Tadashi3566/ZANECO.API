namespace ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

public class TimeLogDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType EmployeeId { get; set; }
    public string EmployeeName { get; set; } = default!;
    public string LogType { get; set; } = default!;
    public DateTime? LogDate { get; set; }
    public DateTime? LogDateTime { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string? ImagePath { get; set; }
}