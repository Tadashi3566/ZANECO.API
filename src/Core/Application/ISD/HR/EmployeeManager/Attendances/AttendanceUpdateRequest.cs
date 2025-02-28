using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

public class AttendanceUpdateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
    public DefaultIdType EmployeeId { get; set; } = default!;
    public string? EmployeeName { get; set; }

    public string DayType { get; set; } = default!;
    public DateTime AttendanceDate { get; set; }

    public bool IsOvertime { get; set; } = default!;

    public DateTime ScheduleTimeIn1 { get; set; }
    public DateTime ScheduleTimeOut1 { get; set; }
    public DateTime ScheduleTimeIn2 { get; set; }
    public DateTime ScheduleTimeOut2 { get; set; }

    public DateTime? ActualTimeIn1 { get; set; }
    public DateTime? ActualTimeOut1 { get; set; }
    public DateTime? ActualTimeIn2 { get; set; }
    public DateTime? ActualTimeOut2 { get; set; }

    public int LateMinutes { get; set; }
    public int UnderTimeMinutes { get; set; }
    public double TotalHours { get; set; }
    public double PaidHours { get; set; }

    public string Status { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class AttendanceUpdateRequestValidator : CustomValidator<AttendanceUpdateRequest>
{
}

public class AttendanceUpdateRequestHandler : IRequestHandler<AttendanceUpdateRequest, DefaultIdType>
{
    private readonly IRepositoryWithEvents<Attendance> _repoAttendance;
    private readonly IStringLocalizer<AttendanceUpdateRequestHandler> _localizer;

    public AttendanceUpdateRequestHandler(IRepositoryWithEvents<Attendance> repoAttendance, IStringLocalizer<AttendanceUpdateRequestHandler> localizer) =>
        (_repoAttendance, _localizer) = (repoAttendance, localizer);

    public async Task<DefaultIdType> Handle(AttendanceUpdateRequest request, CancellationToken cancellationToken)
    {
        var attendance = await _repoAttendance.GetByIdAsync(request.Id, cancellationToken);
        _ = attendance ?? throw new NotFoundException($"Attendance {request.Id} not found.");

        DateTime? actualTimeIn1 = default; // = Convert.ToDateTime(request.ActualTimeIn1);
        DateTime? actualTimeOut1 = default; // = Convert.ToDateTime(request.ActualTimeOut1);
        DateTime? actualTimeIn2 = default; // = Convert.ToDateTime(request.ActualTimeIn2);
        DateTime? actualTimeOut2 = default; // = Convert.ToDateTime(request.ActualTimeOut2);

        if (request.ActualTimeIn1 is not null)
            actualTimeIn1 = new DateTime(request.AttendanceDate!.Year, request.AttendanceDate.Month, request.AttendanceDate.Day, request.ActualTimeIn1.Value.Hour, request.ActualTimeIn1.Value.Minute, 0);

        if (request.ActualTimeOut1 is not null)
            actualTimeOut1 = new DateTime(request.AttendanceDate!.Year, request.AttendanceDate.Month, request.AttendanceDate.Day, request.ActualTimeOut1.Value.Hour, request.ActualTimeOut1.Value.Minute, 0);

        if (request.ActualTimeIn2 is not null)
            actualTimeIn2 = new DateTime(request.AttendanceDate!.Year, request.AttendanceDate.Month, request.AttendanceDate.Day, request.ActualTimeIn2.Value.Hour, request.ActualTimeIn2.Value.Minute, 0);

        if (request.ActualTimeOut2 is not null)
            actualTimeOut2 = new DateTime(request.AttendanceDate!.Year, request.AttendanceDate.Month, request.AttendanceDate.Day, request.ActualTimeOut2.Value.Hour, request.ActualTimeOut2.Value.Minute, 0);

        var updatedAttendance = attendance.Update(request.DayType, request.ScheduleTimeIn1!, request.ScheduleTimeOut1!, request.ScheduleTimeIn2!, request.ScheduleTimeOut2!, actualTimeIn1, actualTimeOut1, actualTimeIn2, actualTimeOut2, request.IsOvertime, request.PaidHours, request.Status, request.Description, request.Notes);

        await _repoAttendance.UpdateAsync(updatedAttendance, cancellationToken);

        return request.Id;
    }
}