namespace ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

public class TimeLogDto : IDto
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType EmployeeId { get; set; } = default!;
    public string EmployeeName { get; set; } = default!;
    public string? Device { get; set; } = string.Empty;
    public string LogType { get; set; } = default!;
    public DateTime LogDate { get; set; } = default!;
    public DateTime LogDateTime { get; set; } = default!;
    public int SyncId { get; set; } = default!;
    public DateTime? SyncDateTime { get; set; } = default!;
    public string? Coordinates { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? ImagePath { get; set; }
}