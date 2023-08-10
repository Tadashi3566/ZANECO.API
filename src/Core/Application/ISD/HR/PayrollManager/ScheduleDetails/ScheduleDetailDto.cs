namespace ZANECO.API.Application.ISD.HR.PayrollManager.ScheduleDetails;

public class ScheduleDetailDto : DtoExtension, IDto
{
    public DefaultIdType ScheduleId { get; set; }
    public string ScheduleName { get; set; } = default!;
    public string ScheduleType { get; set; } = default!; // DAYOFF, WORK
    public string Day { get; set; } = default!; // MONDAY, TUESDAY, WEDNESDAY ...
    public string TimeIn1 { get; set; } = default!;
    public string TimeOut1 { get; set; } = default!;
    public string TimeIn2 { get; set; } = default!;
    public string TimeOut2 { get; set; } = default!;
    public int TotalHours { get; set; } = default!;
}