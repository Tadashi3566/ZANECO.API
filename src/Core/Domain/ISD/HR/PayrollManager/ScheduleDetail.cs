namespace ZANECO.API.Domain.ISD.HR.PayrollManager;

public class ScheduleDetail : AuditableEntity, IAggregateRoot
{
    public ScheduleDetail()
    {
    }

    public virtual Schedule Schedule { get; private set; } = default!;
    public DefaultIdType ScheduleId { get; private set; }
    public string ScheduleName { get; private set; } = default!;
    public string ScheduleType { get; private set; } = default!; // DAYOFF, WORK
    public string Day { get; private set; } = default!; // MONDAY, TUESDAY, WEDNESDAY ...
    public string TimeIn1 { get; private set; } = default!;
    public string TimeOut1 { get; private set; } = default!;
    public string TimeIn2 { get; private set; } = default!;
    public string TimeOut2 { get; private set; } = default!;
    public int TotalHours { get; private set; }

    public ScheduleDetail(DefaultIdType scheduleId, string scheduleName, string scheduleType, string day, string timeIn1, string timeOut1, string timeIn2, string timeOut2, int totalHours, string? description = null, string? notes = null)
    {
        ScheduleId = scheduleId;
        ScheduleName = scheduleName;
        ScheduleType = scheduleType;
        Day = day;

        TimeIn1 = timeIn1;
        TimeOut1 = timeOut1;
        TimeIn2 = timeIn2;
        TimeOut2 = timeOut2;

        TotalHours = totalHours;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();
    }

    public ScheduleDetail Update(string scheduleName, string scheduleType, string day, string timeIn1, string timeOut1, string timeIn2, string timeOut2, int totalHours, string? description = null, string? notes = null)
    {
        if (scheduleName is not null && !ScheduleName.Equals(scheduleName)) ScheduleName = scheduleName;
        if (scheduleType is not null && !ScheduleType.Equals(scheduleType)) ScheduleType = scheduleType;

        if (day is not null && !Day.Equals(day)) Day = day;
        if (timeIn1 is not null && !TimeIn1.Equals(timeIn1)) TimeIn1 = timeIn1;
        if (timeOut1 is not null && !TimeOut1.Equals(timeOut1)) TimeOut1 = timeOut1;
        if (timeIn2 is not null && !TimeIn2.Equals(timeIn2)) TimeIn2 = timeIn2;
        if (timeOut2 is not null && !TimeOut2.Equals(timeOut2)) TimeOut2 = timeOut2;

        if (!TotalHours.Equals(totalHours)) TotalHours = totalHours;

        if (description is not null && (Description?.Equals(description) != true)) Description = description.Trim();
        if (notes is not null && (Notes?.Equals(notes) != true)) Notes = notes.Trim();

        return this;
    }
}