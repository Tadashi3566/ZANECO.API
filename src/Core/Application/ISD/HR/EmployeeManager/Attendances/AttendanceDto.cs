namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

public class AttendanceDto : DtoExtension, IDto
{
    public DefaultIdType EmployeeId { get; set; } = default!;
    public string? EmployeeName { get; set; }

    public string DayType { get; set; } = default!;
    public DateTime AttendanceDate { get; set; } = default!;
    public DateTime? TimeOutDate { get; set; }
    public bool IsOvertime { get; set; } = default!;

    public DefaultIdType ScheduleDetailId { get; set; } = default!;
    public string ScheduleDetailDay { get; set; } = default!;
    public int ScheduleHours { get; set; } = default!;

    public DateTime ScheduleTimeIn1 { get; set; } = default!;
    public DateTime ScheduleTimeOut1 { get; set; } = default!;
    public DateTime ScheduleTimeIn2 { get; set; } = default!;
    public DateTime ScheduleTimeOut2 { get; set; } = default!;

    public DateTime? ActualTimeIn1 { get; set; }
    public DateTime? ActualTimeOut1 { get; set; }
    public DateTime? ActualTimeIn2 { get; set; }
    public DateTime? ActualTimeOut2 { get; set; }

    public int LateMinutes { get; set; } = default!;
    public int UnderTimeMinutes { get; set; } = default!;
    public double TotalHours { get; set; } = default!;
    public double PaidHours { get; set; } = default!;

    public string? ImagePathIn1 { get; set; }
    public string? ImagePathOut1 { get; set; }
    public string? ImagePathIn2 { get; set; }
    public string? ImagePathOut2 { get; set; }
}