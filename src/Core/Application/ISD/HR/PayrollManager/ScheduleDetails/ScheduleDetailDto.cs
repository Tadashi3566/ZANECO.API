namespace ZANECO.API.Application.ISD.HR.PayrollManager.ScheduleDetails;

public class ScheduleDetailDto : IDto
{
    public DefaultIdType Id { get; set; }

    public DefaultIdType ScheduleId { get; set; }
    public string ScheduleName { get; set; } = default!;
    public string ScheduleType { get; set; } = default!; // DAYOFF, WORK
    public string Day { get; set; } = default!; // MONDAY, TUESDAY, WEDNESDAY ...
    public string TimeIn1 { get; set; } = string.Empty;
    public string TimeOut1 { get; set; } = string.Empty;
    public string TimeIn2 { get; set; } = string.Empty;
    public string TimeOut2 { get; set; } = string.Empty;
    public int TotalHours { get; set; } = default!;

    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
}