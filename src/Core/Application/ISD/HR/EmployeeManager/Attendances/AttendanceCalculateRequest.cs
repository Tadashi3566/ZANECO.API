using ZANECO.API.Domain.ISD.HR.EmployeeManager;

namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

public class AttendanceCalculateRequest : IRequest<DefaultIdType>
{
    public DefaultIdType Id { get; set; }
}

public class AttendanceCalculateRequestValidator : CustomValidator<AttendanceCalculateRequest>
{
    public AttendanceCalculateRequestValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty();
    }
}

public class AttendanceCalculateRequestHandler : IRequestHandler<AttendanceCalculateRequest, DefaultIdType>
{
    private readonly IRepositoryWithEvents<Attendance> _repoAttendance;

    public AttendanceCalculateRequestHandler(IRepositoryWithEvents<Attendance> repoAttendance) => _repoAttendance = repoAttendance;

    public async Task<DefaultIdType> Handle(AttendanceCalculateRequest request, CancellationToken cancellationToken)
    {
        var attendance = await _repoAttendance.GetByIdAsync(request.Id, cancellationToken);
        _ = attendance ?? throw new NotFoundException("Attendance not found.");

        var schedTimeIn1 = attendance.ScheduleTimeIn1;
        var schedTimeOut1 = attendance.ScheduleTimeOut1;
        var schedTimeIn2 = attendance.ScheduleTimeIn2;
        var schedTimeOut2 = attendance.ScheduleTimeOut2;

        var attendanceFunction = new AttendanceFunctions();
        int lateMinutes1 = default!;
        int lateMinutes2 = default!;
        int undertimeMinutes1 = default!;
        int undertimeMinutes2 = default!;
        double workingHours1 = default!;
        double workingHours2 = default!;

        if (attendance.DayType.Equals("ON-DUTY"))
        {
            //Check if day attendance is complete
            if (attendance.ActualTimeIn1 is not null && attendance.ActualTimeOut1 is not null && attendance.ActualTimeIn2 is not null && attendance.ActualTimeOut2 is not null)
            {
                lateMinutes1 = attendanceFunction.GetLate(schedTimeIn1, (DateTime)attendance.ActualTimeIn1);
                undertimeMinutes1 = attendanceFunction.GetUnderTime(schedTimeOut1, (DateTime)attendance.ActualTimeOut1);
                workingHours1 = attendanceFunction.GetWorkingHours(schedTimeIn1, (DateTime)attendance.ActualTimeIn1, schedTimeOut1, (DateTime)attendance.ActualTimeOut1, lateMinutes1, undertimeMinutes1);

                lateMinutes2 = attendanceFunction.GetLate(schedTimeIn2, (DateTime)attendance.ActualTimeIn2!);
                undertimeMinutes2 = attendanceFunction.GetUnderTime(schedTimeOut2, (DateTime)attendance.ActualTimeOut2);
                workingHours2 = attendanceFunction.GetWorkingHours(schedTimeIn2, (DateTime)attendance.ActualTimeIn2, schedTimeOut2, (DateTime)attendance.ActualTimeOut2, lateMinutes2, undertimeMinutes2, attendance.IsOvertime);
            }
            else if (attendance.ActualTimeIn1 is not null && attendance.ActualTimeOut2 is not null)
            {
                lateMinutes1 = attendanceFunction.GetLate(schedTimeIn1, (DateTime)attendance.ActualTimeIn1);
                undertimeMinutes1 = attendanceFunction.GetUnderTime(schedTimeOut2, (DateTime)attendance.ActualTimeOut2!);
                workingHours1 = attendanceFunction.GetWorkingHours(schedTimeIn1, (DateTime)attendance.ActualTimeIn1, schedTimeOut2, (DateTime)attendance.ActualTimeOut2, lateMinutes1, undertimeMinutes1, attendance.IsOvertime);
            }
        }
        else
        {
            if (attendance.ActualTimeIn1 is not null && attendance.ActualTimeOut1 is not null && attendance.ActualTimeIn2 is not null && attendance.ActualTimeOut2 is not null)
            {
                workingHours1 = attendanceFunction.GetWorkingHours(schedTimeIn1, (DateTime)attendance.ActualTimeIn1, schedTimeOut1, (DateTime)attendance.ActualTimeOut1, isOverTime: attendance.IsOvertime);
                workingHours2 = attendanceFunction.GetWorkingHours(schedTimeIn2, (DateTime)attendance.ActualTimeIn2, schedTimeOut2, (DateTime)attendance.ActualTimeOut2, isOverTime: attendance.IsOvertime);
            }
            else if (attendance.ActualTimeIn1 is not null && attendance.ActualTimeOut2 is not null)
            {
                workingHours1 = attendanceFunction.GetWorkingHours(schedTimeIn1, (DateTime)attendance.ActualTimeIn1, schedTimeOut2, (DateTime)attendance.ActualTimeOut2, isOverTime: attendance.IsOvertime);
            }
        }

        var calculatedAttendance = attendance.Calculate(attendance.IsOvertime, lateMinutes1 + lateMinutes2, undertimeMinutes1 + undertimeMinutes2, workingHours1 + workingHours2);

        await _repoAttendance.UpdateAsync(calculatedAttendance, cancellationToken);

        return request.Id;
    }
}