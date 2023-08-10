namespace ZANECO.API.Application.ISD.HR.EmployeeManager.TimeLogs;

public class TimeLogDto : BaseDto, IDto
{
    public DefaultIdType EmployeeId { get; set; } = default!;
    public string EmployeeName { get; set; } = default!;
    public string LogType { get; set; } = default!;
    public DateTime LogDate { get; set; } = default!;
    public DateTime LogDateTime { get; set; } = default!;
    public int SyncId { get; set; } = default!;
    public DateTime? SyncDateTime { get; set; } = default!;
    public string? Coordinates { get; set; }
}