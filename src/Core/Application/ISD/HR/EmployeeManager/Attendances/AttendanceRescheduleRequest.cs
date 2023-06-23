using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

public class AttendanceRescheduleRequest : IRequest<bool>
{
    public DefaultIdType EmployeeId { get; set; } = default!;
    public DateTime AttendanceDate { get; set; } = default!;
    public DateTime UpToDate { get; set; } = default!;
    public DateTime ScheduleTimeIn1 { get; set; } = default!;
    public DateTime ScheduleTimeOut1 { get; set; } = default!;
    public DateTime ScheduleTimeIn2 { get; set; } = default!;
    public DateTime ScheduleTimeOut2 { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}

public class AttendanceRescheduleRequestValidator : CustomValidator<AttendanceRescheduleRequest>
{
    public AttendanceRescheduleRequestValidator()
    {
        RuleFor(p => p.EmployeeId)
            .NotEmpty();
    }
}

public class AttendanceRescheduleRequestHandler : IRequestHandler<AttendanceRescheduleRequest, bool>
{
    private readonly IRepositoryWithEvents<Attendance> _repoAttendance;
    private readonly IStringLocalizer<AttendanceRescheduleRequestHandler> _localizer;

    public AttendanceRescheduleRequestHandler(IRepositoryWithEvents<Attendance> repoAttendance, IStringLocalizer<AttendanceRescheduleRequestHandler> localizer) =>
        (_repoAttendance, _localizer) = (repoAttendance, localizer);

    public async Task<bool> Handle(AttendanceRescheduleRequest request, CancellationToken cancellationToken)
    {
        DateTime attendanceDate = request.AttendanceDate;

        while (attendanceDate <= request.UpToDate)
        {
            var attendance = await _repoAttendance.FirstOrDefaultAsync(new AttendanceByDateSpec(request.EmployeeId, attendanceDate), cancellationToken);
            _ = attendance ?? throw new NotFoundException("Attendance not found.");

            var updatedSchedule = attendance.Reschedule(request.ScheduleTimeIn1, request.ScheduleTimeOut1, request.ScheduleTimeIn2, request.ScheduleTimeOut2, request.Description, request.Notes);

            await _repoAttendance.UpdateAsync(updatedSchedule, cancellationToken);

            attendanceDate = attendanceDate.AddDays(1);
        }

        return true;
    }
}